<script>
        /* Source: https://gist.github.com/lamberta/3768814
    * Parse a string function definition and return a function object. Does not use eval.
    * @param {string} str
    * @return {function}
    *
    * Example:
    *  var f = function (x, y) { return x * y; };
    *  var g = parseFunction(f.toString());
         *  g(33, 3); //=> 99
    */
    function parseFunction(str) {
            if (!str) return void (0);

    var fn_body_idx = str.indexOf('{'),
                fn_body = str.substring(fn_body_idx + 1, str.lastIndexOf('}')),
    fn_declare = str.substring(0, fn_body_idx),
    fn_params = fn_declare.substring(fn_declare.indexOf('(') + 1, fn_declare.lastIndexOf(')')),
    args = fn_params.split(',');

    args.push(fn_body);

    function Fn() {
                return Function.apply(this, args);
            }
    Fn.prototype = Function.prototype;

    return new Fn();
        }

    // get uri all query variables
    function getQueryVariables(uri) {
            var searchArgs = window.location.search;
    if (uri) {
                var s = uri.indexOf('?');
                searchArgs = s > -1 ? uri.substring(s) : '';
            }
    var query = searchArgs.substring(1);
    var vars = query.split('&');
    var varObj = { };
    for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split('=');
    varObj[pair[0]] = pair[1];
            }
    return varObj;
        }

    // get uri query variable
    function getQueryVariable(variable, uri) {
            var vars = getQueryVariables(uri);
    for (var key in vars) {
                if (key === variable) return vars[key];
            }
    return (false);
        }

    // if value is null or undefined than execute delete operate, else execute update
    function updateQueryVariable(uri, key, value) {
            var re = new RegExp('([?&])' + key + '=.*?(&|$)', 'i');
    var separator = uri.indexOf('?') !== -1 ? '&' : '?';
    if (uri.match(re)) {
                return uri.replace(re, !value ? '' : ('$1' + key + '=' + value + '$2'));
            }
    else {
                return !value ? '' : (uri + separator + key + '=' + value);
            }
        }

    // get uri template
    function getUrlTemplate(uri) {
            var reg = /\{(.+?)\}/g;
    return uri.match(reg);
        }

    // hide empty tags
    var HideEmptyTagsPlugin = function HideEmptyTagsPlugin() {
            return {
        statePlugins: {
        spec: {
        wrapSelectors: {
        taggedOperations: function taggedOperations(ori) {
                                return function () {
                                    return ori.apply(void 0, arguments).filter(function (tagMeta) {
                                        return tagMeta.get("operations") && tagMeta.get("operations").size > 0;
                                    });
                                };
                            }
                        }
                    }
                }
            };
        };

    var tokenKey = "access-token";
    var cultureKey = "culture";

    // default response interceptor
    function defaultResponseInterceptor(response) {
            // handle jwt token
            var accessToken = response.headers[tokenKey];
    if (accessToken && accessToken != "invalid_token") {
        window.localStorage.setItem(tokenKey, accessToken);
    ui.preauthorizeApiKey("Bearer", accessToken);
            }
    else if (accessToken == "invalid_token") {
        window.localStorage.removeItem(tokenKey);
    ui.authActions.logout(["Bearer"]);
            }

    return response;
        }

    // default request interceptor
    function defaultRequestInterceptor(request) {
            var url = request.url;

    // add swagger header
    request.headers["request-from"] = "swagger";

    // handle template
    var temps = getUrlTemplate(url);
            if (temps && temps.length > 0) {
                for (var i = 0; i < temps.length; i++) {
                    var temp = temps[i];
    var key = temp.substring(1, temp.length - 1);
    var queryKey = getQueryVariable(key, url);
    if (queryKey) {
        url = updateQueryVariable(url.replace(temp, queryKey), key);
                    }
    else url = url.replace(temp, '');
                }
    request.url = url;
            }

    // handle culture lang
    var culture = getQueryVariable(cultureKey);
            if (culture && culture.length > 0) {
        request.url = updateQueryVariable(url, cultureKey, culture);
            }

    return request;
        }

    // send ajax request
    function sendRequest(params) {
        let xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
        let obj = JSON.parse(xhr.response);
    let headers = xhr.getAllResponseHeaders();
    let arr = headers.trim().split(/[\r\n]+/);
    let headerMap = { };
    arr.forEach(function (line) {
        let parts = line.split(': ');
    let header = parts.shift();
    let value = parts.join(': ');
    headerMap[header] = value;
                    });

    params.success(obj, headerMap);
                } else if (xhr.readyState == 4 && xhr.status >= 400) {
        params.error(xhr);
                }
            }

    function objectToString(obj) {
        let arr = [];
    for (var k in obj) {
        arr.push(`${k}=${obj[k]}`);
                }

    return arr.join('&');
            }

    let qs = objectToString(params.data);

    var accessToken = window.localStorage.getItem(tokenKey);

    if (params.method.toUpperCase() == 'GET') {
        xhr.open('GET', params.url + '?' + qs);
    if (accessToken) {
        xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
                }
    xhr.send();
            } else if (params.method.toUpperCase() == 'POST') {
        xhr.open('POST', params.url);
    if (accessToken) {
        xhr.setRequestHeader('Authorization', 'Bearer ' + accessToken);
                }
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.send(qs);
            }
        }

    // init swagger ui
    function initSwaggerUI(configObject, oauthConfigObject) {
        configObject.onComplete = function () {
            var accessToken = window.localStorage.getItem(tokenKey);
            if (accessToken) {
                ui.preauthorizeApiKey("Bearer", accessToken);
            }
        };

    // Workaround for https://github.com/swagger-api/swagger-ui/issues/5945
    configObject.urls.forEach(function (item) {
                if (item.url.startsWith("http") || item.url.startsWith("/")) return;
    item.url = window.location.href.replace("index.html", item.url).split('#')[0];
            });

    // If validatorUrl is not explicitly provided, disable the feature by setting to null
    if (!configObject.hasOwnProperty("validatorUrl"))
    configObject.validatorUrl = null

    // If oauth2RedirectUrl isn't specified, use the built-in default
    if (!configObject.hasOwnProperty("oauth2RedirectUrl"))
    configObject.oauth2RedirectUrl = (new URL("oauth2-redirect.html", window.location.href)).href;

    // Apply mandatory parameters
    configObject.dom_id = "#swagger-ui";
    configObject.presets = [SwaggerUIBundle.presets.apis, SwaggerUIStandalonePreset, HideEmptyTagsPlugin];
    configObject.layout = "StandaloneLayout";

    // Parse and add interceptor functions
    var interceptors = JSON.parse('{"RequestInterceptorFunction":"(request) =\u003E { return defaultRequestInterceptor(request); }","ResponseInterceptorFunction":"(response) =\u003E { return defaultResponseInterceptor(response); }"}');
    if (interceptors.RequestInterceptorFunction)
    configObject.requestInterceptor = parseFunction(interceptors.RequestInterceptorFunction);
    if (interceptors.ResponseInterceptorFunction)
    configObject.responseInterceptor = parseFunction(interceptors.ResponseInterceptorFunction);

    // Begin Swagger UI call region

    const ui = SwaggerUIBundle(configObject);

    ui.initOAuth(oauthConfigObject);

    // End Swagger UI call region

    window.ui = ui
        }

    window.onload = function () {
            var configObject = JSON.parse('{"urls":[{"url":"/swagger/%E5%8D%9A%E5%AE%A2%E5%8A%9F%E8%83%BD/swagger.json","name":"\u535A\u5BA2\u529F\u80FD"},{"url":"/swagger/%E5%B7%A5%E5%85%B7%E5%85%83%E7%B4%A0/swagger.json","name":"\u5DE5\u5177\u5143\u7D20"},{"url":"/swagger/Default/swagger.json","name":"\u540E\u53F0\u7BA1\u7406\u89C4\u8303\u5316\u63A5\u53E3"}],"deepLinking":false,"persistAuthorization":false,"displayOperationId":false,"defaultModelsExpandDepth":1,"defaultModelExpandDepth":1,"defaultModelRendering":"example","displayRequestDuration":false,"docExpansion":"none","showExtensions":false,"showCommonExtensions":false,"supportedSubmitMethods":["get","put","post","delete","options","head","patch","trace"],"tryItOutEnabled":false}');
    var oauthConfigObject = JSON.parse('{"scopeSeparator":" ","scopes":[],"useBasicAuthenticationWithAccessCodeGrant":false,"usePkceWithAuthorizationCodeGrant":false}');

    var loginObject = configObject.LoginInfo;
    if (loginObject && loginObject.enabled === true) {
                var loginForm = document.getElementById("login-form");
    var userName = document.getElementById("userName");
    var password = document.getElementById("password");
    var submit = document.getElementById("submit");
    var loginError = document.getElementById("login-error");
    loginForm.style.display = "block";

    sendRequest({
        method: "POST",
    url: loginObject.checkUrl,
    success: function (res, headerMap) {
                        if (res.toString() === "200") {
        loginForm.style.display = "none";
    initSwaggerUI(configObject, oauthConfigObject);
                        }
    else {
        userName.focus();

    submit.addEventListener("click", function (ev) {
                                if (userName.value.trim().length === 0) {
        userName.focus();
    return;
                                }
    if (password.value.trim().length === 0) {
        password.focus();
    return;
                                }

    loginError.innerHTML = "";

    sendRequest({
        method: "POST",
    url: loginObject.submitUrl,
    data: {
        userName: userName.value.trim(),
    password: password.value.trim()
                                    },
    success: function (res, headerMap) {
                                        if (res.toString() === "200") {
        loginForm.style.display = "none";
    initSwaggerUI(configObject, oauthConfigObject);
    defaultResponseInterceptor({headers: headerMap });
                                        }
    else {
        loginError.innerHTML = "Invalid UserName or Password.";
                                        }
                                    },
    error: function (xhr) {
                                        if (xhr.status === 404) {
        loginError.innerHTML = "Not Found: " + loginObject.submitUrl;
                                        }
    else {
        loginError.innerHTML = "Internal ServerError: " + loginObject.submitUrl;
                                        }
                                    }
                                });
                            });
                        }
                    },
    error: function (xhr) {
                        if (xhr.status === 404) {
        loginError.innerHTML = "Not Found: " + loginObject.checkUrl;
                        }
    else {
        loginError.innerHTML = "Internal ServerError: " + loginObject.checkUrl;
                        }
                    }
                });
            }
    else {
        initSwaggerUI(configObject, oauthConfigObject);
            }
        }
</script>
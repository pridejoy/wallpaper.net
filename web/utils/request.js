import Request from "luch-request"

// 这里填写你后端的api地址 
let baseUrl = "https://localhost:7057"

const http = new Request({
	baseURL: baseUrl,
	timeout: 5000,
	responseType: "application/json",
})
http.interceptors.request.use((config) => {
		/* 请求之前拦截器。可以使用async await 做异步操作 */
	config.header = {
		Authorization: 'Bearer ' + uni.getStorageSync("token"),
	}

	config.header = {
		...config.header
	}
	return config
})
http.interceptors.response.use((response) => {
	return response.data
}, (response) => {
	if (response.statusCode === 200 && response.data.code === 401) {
		 uni.navigateTo({
			url: '/pageA/login/login'
		 })
	}
	if (response.statusCode === 400) {
		uni.showToast({
			title: response.data.msg || '服务器错误',
			icon: "none",
			duration: 3000,
		})
	} 
	else if (response.statusCode === 404) {
		uni.showToast({
			title: "404",
			icon: "none",
			duration: 3000,
		})
	} 
	if (response.statusCode === 500) {
		uni.showToast({
			title: response.data.msg || '服务器错误',
			icon: "none",
			duration: 3000,
		})
	} else if (response.statusCode === 400) {
		uni.showToast({
			title: response.data.msg,
			icon: "none",
			duration: 3000,
		})
	} else if (response.statusCode === 422) {
		uni.showToast({
			title: response.data.msg,
			icon: "none",
			duration: 3000,
		})
	} else if (response.statusCode === 401) {

	} else if (response.statusCode === 403) {
		uni.showToast({
			title: response.data.msg,
			icon: "none",
			duration: 3000,
		})
	} else if (response.errMsg === "request:fail timeout") {
		uni.showToast({
			title: "网络请求超时",
			icon: "none",
			duration: 3000,
		})
	} else {
		uni.showToast({
			title: "请求异常 刷新重试",
			icon: "none",
			duration: 3000,
		})
	}
})
export default http

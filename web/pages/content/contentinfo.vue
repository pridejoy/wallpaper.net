<template>
	<view style="background-color:#FFFFFF;">
		<tn-nav-bar fixed>文章详情</tn-nav-bar>
		<view :style="{paddingTop: vuex_custom_bar_height + 'px'}">
			<!-- <view :style="{backgroundImage:'url('+activedata.articleCover+')'}"> </viwe> -->
	    	<image class="activleimg" :src="activedata.articleCover"></image>

			
			<!-- 标题 -->
			<view class="titel-h-w">
				<view class="titel-h">
					{{ activedata.title }}
				</view>
			</view>
			
			<!-- 时间和装饰品 -->
			<view class="time-view">
				<view class="time-img">
					<image class="fengrui-img" src="../../static/images/data/time.svg" mode="aspectFit"></image>
				</view>
				<view class="time-font">
			    	{{activedata.createdTime}}
				</view>
			</view>
			
			
			<!-- 正文内容 -->
			<view class="data-view">
				<!-- <mp-html :tag-style="tag_style" :content="html" :container-style="container_style" selectable="true"> 内容疯狂加载ing...</mp-html> -->
			</view>
		</view>

	</view>
</template>

<script>
	// import mpHtml from "mp-html/dist/uni-app/components/mp-html/mp-html";
	// import {
	// 	marked
	// } from "marked";
	import hljs from "highlight.js";
	import "highlight.js/scss/atom-one-dark.scss";
	// import {
	// 	getPageinfo
	// } from '@/api/blog.js';


	export default {
		components: {
			// mpHtml,
		},
		data() {
			return {
				articleId: 0,
				html: "", //存放makedown转换的html 
				activedata: {} ,//文章内容
				// 文本框解析
				tag_style: {
					h1: 'line-height: 50px;font-size:16px;',
					h2: 'line-height: 50px;font-size:16px',
					h3: 'line-height: 50px;font-size:16px',
					h4: 'line-height: 50px;font-size:16px',
				},
				container_style: 'font-size:15px;overflow-x: hidden;',
			}
		},
		onLoad(option) {
			let {
				articleId
			} = option
			this.articleId = articleId;
			this.getActiveInfo();
		},
		mounted() {
			// 将文本转化成html
			// this.html = marked(this.markdown);
			// 下面代码是解决代码高亮无效问题
			// this.html = marked(this.markdown).replace(/<pre>/g, "<pre class='hljs'>");
		},
		methods: {
			/**
			 * 代码高亮
			 * */
		// 	initHighLight() {
		
		// 		hljs.configure({
		// 			    tabReplace:'',// 默认什么都不加，4个空格
		// 			    classPrefix:''//不要附加类前缀，默认前缀是'hljs-' 
		// 		});
		// 		marked.setOptions({
		// 			renderer: new marked.Renderer(),
		// 			gfm: true,
		// 			tables: true,
		// 			breaks: false,
		// 			pedantic: false,
		// 			highlight: function(code, lang) {
		// 						debugger
		// 				if (lang && hljs.getLanguage(lang)) {
		// 					return hljs.highlight(lang, code, true).value;
		// 				} else {
		// 					return hljs.highlightAuto(code).value;
		// 				}
		// 			},
		// 		});
		// 	},

			/**
			 * 获取当前文章详情
			 * */
			getActiveInfo() {
				let param = {
					articleId: this.articleId
				};
				// getPageinfo(param).then(res => {
				// 	if (res.data.success && res.data.code == 200) {
				// 		this.activedata = res.data.data 
				// 		this.html = marked(this.activedata.articleContent);
				// 		this.html=this.html.replace(/<h1([\s\w"=\/\.:;]+)((?:(style="[^"]+")))/ig, '<h1')
				// 					.replace(/<h1([\s\w"=\/\.:;]+)((?:(class="[^"]+")))/ig, '<h1')
				// 					.replace(/<h1>/ig, '<h1 class="h1">')

				// 					.replace(/<h2([\s\w"=\/\.:;]+)((?:(style="[^"]+")))/ig, '<h2')
				// 					.replace(/<h2([\s\w"=\/\.:;]+)((?:(class="[^"]+")))/ig, '<h2')
				// 					.replace(/<h2>/ig, '<h2 class="h2">')

				// 					.replace(/<h3([\s\w"=\/\.:;]+)((?:(style="[^"]+")))/ig, '<h3')
				// 					.replace(/<h3([\s\w"=\/\.:;]+)((?:(class="[^"]+")))/ig, '<h3')
				// 					.replace(/<h3>/ig, '<h3 class="h3">')

				// 					.replace(/<h4([\s\w"=\/\.:;]+)((?:(style="[^"]+")))/ig, '<h4')
				// 					.replace(/<h4([\s\w"=\/\.:;]+)((?:(class="[^"]+")))/ig, '<h4')
				// 					.replace(/<h4>/ig, '<h4 class="h4">')

				// 					.replace(/<h5([\s\w"=\/\.:;]+)((?:(style="[^"]+")))/ig, '<h5')
				// 					.replace(/<h5([\s\w"=\/\.:;]+)((?:(class="[^"]+")))/ig, '<h5')
				// 					.replace(/<h5>/ig, '<h5 class="h4">')

				// 					.replace(/<h6([\s\w"=\/\.:;]+)((?:(style="[^"]+")))/ig, '<h6')
				// 					.replace(/<h6([\s\w"=\/\.:;]+)((?:(class="[^"]+")))/ig, '<h6')
				// 					.replace(/<h6>/ig, '<h6 class="h6">')

				// 					.replace(/<code([\s\w"=\/\.:;]+)((?:(style="[^"]+")))/ig, '<code')
				// 					.replace(/<code([\s\w"=\/\.:;]+)((?:(class="[^"]+")))/ig, '<code')
				// 					.replace(/<code>/ig, '<code class="language-html" style="    border-radius: 25px;" >') //解决代码样式
				// 					.replace(/<pre>/g, "<pre class='hljs'>") 
				// 		console.log("",this.html )
				// 	} else {
				// 		console.log("获取数据失败了")
				// 	}
				// }).catch(resolve => {
				// 	console.log("获取数据异常了", resolve)
				// })
			}
		},
	}
</script>

<style scoped>
	
 
	/* 头图 */
.activleimg{
	width: 100%;
}
	
	
	/* 富文本解析视频宽度 */
	.video {
		width: 100% !important;
		margin-top: 10px;
	}
	/* 文章样式 */
 .titel-h-w {
 	display: flex;
 	justify-content: space-between;
 	align-items: center;
 	margin: 50upx 30upx 0upx 30upx;
 	flex-grow: 1;
 }
 
 .titel-h {
 	font-size: 38upx;
 	font-weight: bold;
 }
 
	/* 时间样式 */
	.fengrui-img {
		height: 100%;
		width: 100%;
	}
	
	.time-font {
		font-weight: 400;
		font-size: 24upx;
		color: rgba(124, 124, 124, 1);
		margin-left: 10upx;
	}
	
	.time-img {
		height: 40upx;
		width: 40upx;
	}
	
	.time-view {
		margin: 30upx 28upx;
		display: flex;
		align-items: center;
		justify-content: flex-start;
	}
	
	
	/* 文章内容样式 */
	
	.data-view {
		margin: 20upx 20upx;
		font-size: 34upx;
		color: #333232;
		line-height: 60upx; //文字行高
	
	}
	
.titel-h-go {
		font-size: 32upx;
		margin-left: 20upx;
	}

	/* 金刚区域 */
	.district-poster {
		width: 220upx;
		height: 60upx;
		border-radius: 200upx;
		background: linear-gradient(90deg, rgba(67, 130, 235, 1) 0%, rgba(6, 189, 254, 1) 100%);
		text-align: center;
		line-height: 64upx;
		color: #FFFFFF;
		font-size: 28upx;
		margin: 0upx 20upx;
		box-shadow: #4284ec 0upx 4upx 12upx;
	}

	.district-fenrid {
		width: 220upx;
		height: 60upx;
		border-radius: 200upx;
		background: linear-gradient(90deg, #F86168, #FFC6D3 100%);
		text-align: center;
		line-height: 64upx;
		color: #FFFFFF;
		font-size: 28upx;
		margin: 0upx 20upx;
		box-shadow: #F86168 0upx 4upx 12upx;
	}

	.district-money {
		width: 220upx;
		height: 60upx;
		border-radius: 200upx;
		background: linear-gradient(90deg, rgba(12, 185, 162, 1) 0%, rgba(15, 236, 210, 1) 100%);
		text-align: center;
		line-height: 64upx;
		color: #FFFFFF;
		font-size: 28upx;
		margin: 0upx 20upx;
		box-shadow: #0bbda6 0upx 4upx 12upx;
	}

	.district-fabulous {
		width: 180upx;
		height: 60upx;
		border-radius: 200upx;
		background: linear-gradient(90deg, rgba(248, 97, 104, 1) 0%, rgba(255, 198, 211, 1) 100%);
		text-align: center;
		line-height: 64upx;
		color: #FFFFFF;
		font-size: 28upx;
		margin: 0upx 16upx;
		box-shadow: #f96a71 0upx 6upx 22upx;
	}

	.district {
		display: flex;
		align-items: center;
		justify-content: center;
	}

	/* 评语 */
	.span-color {
		color: red;
	}

	.review {
		background-color: #f9f9f9;
		padding: 32upx;
		margin: 20upx 30upx;
		border-radius: 10upx;
		color: #807C7C;
		font-size: 24upx;
	}

	/* 正文内容 */
	.data-font {
		margin-top: 40upx;
	}

	.data-h:after {
		content: '';
		position: absolute;
		bottom: -14upx;
		left: 0px;
		width: 96upx;
		height: 8upx;
		border-radius: 200upx;
		background: linear-gradient(90deg, rgba(55, 110, 234, 1) 0%, rgba(255, 255, 255, 1) 100%);
	}

	.data-h {
		color: #000000;
		font-size: 32upx;
		position: relative;
	}

	.data-img {
		border-radius: 10upx;
		width: 100%;
		height: auto;
		overflow: hidden;
		margin: 30upx 0upx;
	}

	.data-view {
		margin: 20upx 20upx;
		font-size: 34upx;
		color: #333232;
		line-height: 60upx; //文字行高

	}

	/* 时间和浏览量 */

	.time-le {
		margin-left: 20upx;
	}

	.ma-left-20 {
		margin-left: 20upx;
	}

	.time-font-2 {
		background-color: #796bf7;
		color: #FFFFFF;
		font-size: 24upx;
		border-radius: 10upx;
		padding: 4upx 20upx;
		margin-left: 10upx;
	}

	.time-font {
		font-weight: 400;
		font-size: 24upx;
		color: rgba(124, 124, 124, 1);
		margin-left: 10upx;
	}

	.time-img {
		height: 40upx;
		width: 40upx;
	}

	.time-view {
		margin: 30upx 28upx;
		display: flex;
		align-items: center;
		justify-content: flex-start;
	}

	/* 标题 */
	.tar-w-h {
		color: #000000;
	}

	.titel-h {
		font-size: 38upx;
		font-weight: bold;
	}

	.titel-view {
		width: 180upx;
		flex-shrink: 0;
	}

	.titel-h-w {
		display: flex;
		justify-content: space-between;
		align-items: center;
		margin: 50upx 30upx 0upx 30upx;
		flex-grow: 1;
	}

	/* 背景图片 */
	.fengrui-img-tar {
		height: 40upx;
		width: 40upx;
	}

	.square-black-img {
		height: 40upx;
		width: 40upx;
		margin-top: 10upx;
	}

	.square-black {
		position: fixed;
		left: 28upx;
		background: rgba(255, 255, 255, 0.5);
		height: 55upx;
		z-index: 2;
		width: 100upx;
		border-radius: 100upx;
		top: 0upx;
		text-align: center;
	}

	.back-radius {
		position: absolute;
		bottom: 0;
		left: 0;
		height: 60upx;
		width: 100%;
		border-radius: 40upx 40upx 0upx 0upx;
		background: #FFFFFF;
		z-index: 2;
	}

	.back-img {
		overflow: hidden;
		position: relative;
		background-color: #3482e2;
		border-radius: 0upx 0upx 40upx 40upx;
		padding-bottom: 20upx;
		height: 20upx;
	}

	.fengrui {
		height: 100%;
		width: 100%;
	}

	/* 自定义标题栏 */
	.tar-img {
		margin-left: 28upx;
		height: 48upx;
		width: 48upx;
		overflow: hidden;
	}

	.tar-t {
		background-color: #FFFFFF;
		position: fixed;
		left: 0;
		width: 100%;
		z-index: 9999;
		display: flex;
		align-items: center;
		justify-content: space-between;
	}

	.tar-w {
		background-color: #FFFFFF;
		position: fixed;
		top: 0;
		left: 0;
		width: 100%;
		z-index: 99999999999;
	}

	.fengrui-img {
		height: 100%;
		width: 100%;
	}

	.index-ov {
		/* overflow-x: hidden; */
	}

	button:after {
		border: 0px solid rgba(0, 0, 0, .2);

	}

	/* 富文本解析视频宽度 */
	.video {
		width: 100% !important;
	}


	/* 暗黑模式下应用的样式 */
	@media (prefers-color-scheme: dark) {
		page,
		.data-login-flex{
			background: #161616;
		}

		.back-radius {
			background: #161616;
		}

		.tar-w {
			background: #161616;
		}

		.tar-t {
			background: #161616;
		}

		.data-h {
			color: #d7d7d7;
		}

		.data-view {
			color: #d7d7d7 !important;
		}
		.me-svg {
			background: #202020;
		}

		.list-view {
			background: #202020;
		}

		.time-font {
			color: rgba(124, 124, 124, 1);
		}

		.fengrui-img-tar {
			color: rgba(201, 201, 201, 1);
		}

		.square-black-img {
			color: rgba(201, 201, 201, 1);
		}

		.tar-w-h {
			color: rgba(201, 201, 201, 1);
		}
	}


</style>

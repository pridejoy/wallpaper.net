<template>
	<view>
		<!-- 每日必应 -->
		<view class="template-photo">
			<!-- 顶部自定义导航 -->
			<tn-nav-bar fixed alpha customBack>
				<view slot="back" class='tn-custom-nav-bar__back' @click="goBack">
					<text class='icon tn-icon-left'></text>
					<text class='icon tn-icon-home-capsule-fill'></text>
				</view>
			</tn-nav-bar>
			<!-- 页面内容 -->
			<view class="slideshow">
				<!-- <view class="slideshow-image" 	style="background-image: url('https://www.bing.com/th?id=OHR.DarkSkyAcadia_ZH-CN1827511700_1920x1080.jpg')"> </view> -->
				<view class="slideshow-image" :style="{backgroundImage:'url('+binginfo.mobileUrl+')'}"> </view>
			</view>
		</view>

		<tn-toast ref="toast"></tn-toast>

		<!-- 		<view id="block">
			<div class="block_button"  @click="goBack">
				<text class="tn-icon-left-arrow"></text>
				<view>返回</view>
			</div>
			<div class="block_button">
				<text class="tn-icon-like" @click="collect()"></text>
				<view>收藏</view>
			</div>
			<div class="block_button" @click="downfile()">
				<text class="tn-icon-download"></text>
				<view>下载</view>
			</div>
		</view> -->


		<view class="bottom_nav">
	<!-- 		<view @click="goBack" class="bottom_nav_item">
				<view class="bottom_nav_item_img_container">
					<text class="tn-icon-left-arrow"></text>
				</view>
				<view class="bottom_nav_item_title">
					返回
				</view>
			</view> -->
			<view @click="collect()" class="bottom_nav_item">
				<view class="bottom_nav_item_img_container">
					<text class="tn-icon-trademark"></text>
				</view>
				<view class="bottom_nav_item_title">
					信息
				</view>
			</view>
			<view @click="computer()" class="bottom_nav_item">
				<view class="bottom_nav_item_img_container">
					<text class="tn-icon-computer"></text>
				</view>
				<view class="bottom_nav_item_title">
					电脑
				</view>
			</view>
			<view @click="imagepreview()" class="bottom_nav_item">
				<view class="bottom_nav_item_img_container">
					<text class="tn-icon-download"></text>
				</view>
				<view class="bottom_nav_item_title">
					预览
				</view>
			</view>
			<!-- #ifdef MP-WEIXIN -->
			<view class="bottom_nav_item" style="position: relative;">
				<view class="bottom_nav_item_img_container">
					<!-- <image style="width: 35rpx;height: 32rpx;" class="bottom_nav_item_img" src="../../static/share.png" >
							</image> -->
					<text class=" tn-icon-share-square" ></text>

				</view>
				<view class="bottom_nav_item_title">
					分享
				</view>
				<button class="bottom_nav_item"
					style="position: absolute;top: 0;left: 0;padding: 0;margin: 0;width: 100%;height: 100%;opacity: 0;"
					open-type="share"></button>
			</view>
			<!-- #endif -->
		</view>

	</view>

</template>

<script>
	import {
		currDayBing
	} from '@/utils/api/element.js';;
	import template_page_mixin from '@/libs/mixin/template_page_mixin.js' 
	export default {
		name: 'TemplatePhoto',
		mixins: [template_page_mixin],
		data() {
			return {
				binginfo: {}, 
			}
		},
		//销毁上一个页面的emit
		onUnload() {
			 
		}, 
		onLoad() {

	       
		},
		methods: {
			/**
			 * 初始化必应壁纸
			 * */
			getEverydayBingpic() {  
		      currDayBing().then(res => {
				if (res.success && res.code == 200) {
					// console.log("获取数据", res.data.data)
					this.binginfo = res.data;
				} else {
					uni.showToast({
						icon: 'error',
						title: '获取数据失败了' + resolve
					});
				}
			}).catch(resolve => {
				uni.showToast({
					icon: 'error',
					title: '获取数据异常了' + resolve
				});
			}) 
			 
			},	//预览图片
			imagepreview() {
				uni.showToast({
					title: '仅供观赏，自觉删除',
					icon: "none",
					duration: 3000,
				})
				var url=this.binginfo.mobileUrl;
				// 预览图片  
				uni.previewImage({
										current: 0,
										urls: [url]
									});
			},
			/**
			 * 下载
			 */
			downfile() {
				let that = this;
				// console.log("当前下载链接" + this.binginfo.mobileUrl);
				// uni.downloadFile({
				// 	url: this.binginfo.mobileUrl,
				// 	success: (res) => {
				// 		if (res.statusCode === 200) {
				// 			console.log(res);
				// 			uni.saveImageToPhotosAlbum({ //保存图片到系统相册。
				// 				filePath: res.tempFilePath, //图片文件路径
				// 				success: function() {
				// 					that.$refs.toast.show({
				// 						title: '保存成功'
				// 					})
				// 				},
				// 				fail: function(e) {
				// 					that.$refs.toast.show({
				// 						title: '保存失败'
				// 					})
				// 				}
				// 			})
				// 		} else {
				// 			that.$refs.toast.show({
				// 				title: '下载失败'
				// 			})
				// 		}
				// 	}
				// });
			},

			// 电脑
			computer() { 
				let imgsArray = [];
				imgsArray.push(this.binginfo.url) 
				uni.previewImage({
					current: 0,
					urls: imgsArray
				}); 
			},
			/**
			 * 提示信息
			 */
			collect() {
				//设置标题为当前的时间
				this.startDate =this.$options.filters['Fdate'](this.binginfo.startDate);
				console.log("时间"+this.startDate);	
				this.$refs.toast.show({
					     title: this.startDate,
					     content: this.binginfo.copyRight,
				})
				uni.vibrateShort({
					success: function() {
						console.log('success');
					}
				});
			}

		},
		created() {   
			this.getEverydayBingpic() 
		}, 
	}
</script>

<style lang="scss" scoped>
	@import '@/static/css/templatePage/custom_nav_bar.scss';

	.bottom_nav {
		left: 2.5%;
		top: 90%;
		width: 95%;
		z-index: 999; 
		height: 100rpx;
		// color: #FFFFFF;
		position: fixed;
		bottom: 0rpx;
		display: flex;
		justify-content: center;
		align-items: center;
		flex-wrap: wrap;
		border-radius: 90rpx; //圆角

		background-color: cadetblue;
		backdrop-filter: saturate(150%) blur(8px);
		-webkit-backdrop-filter: saturate(150%) blur(8px);
		background-color: rgba(255, 255, 255, 0.5);

		.bottom_nav_item {
			width: 25%;
			height: 100rpx;
			text-align: center;
			line-height: 100rpx;
			// text-shadow: 0 0 2px #aa00ff;
			font-size: 25rpx;

			.bottom_nav_item_img_container {
				width: 100%;
				height: 55rpx;
				display: flex;
				justify-content: center;
				align-items: center;

				.bottom_nav_item_img {
					width: 50rpx;
					height: 50rpx;
				}
			}

			.bottom_nav_item_title {
				width: 100%;
				height: 50rpx;
				line-height: 27rpx;
				text-align: center;
			}
		}
	}

	// #block {
	// 	position: absolute;
	// 	top: 90%;
	// 	margin: 0 auto;
	// 	width: 90%;
	// 	height: 100rpx;
	// 	left: 0;
	// 	right: 0;

	// 	z-index: 999;
	// 	background-color: aqua;
	// 	font-size: 15px;

	// 	padding: 10px;
	// 	border-radius: 2em;
	// 	// backdrop-filter: blur(20px);
	// 	background-color: rgba(255, 255, 255, 0.5);


	// 	text-align: center;

	// 	display: grid;
	// 	grid-template-columns: repeat(3, 1fr);
	// 	grid-template-rows: 1fr;
	// 	grid-column-gap: 10px;
	// 	grid-row-gap: 3px;
	// }


	// .block_button {
	// 	flex: 1;
	// }


	.template-photo {
		margin: 0;
		width: 100%;
		height: 100vh;
		color: #fff;
		// overflow: hidden;
	}



	/* 相册 start*/
	.slideshow {
		top: 0;
		position: absolute;
		width: 100vw;
		height: 100vh;
		overflow: hidden;
	}

	.slideshow-image {
		position: absolute;
		width: 100%;
		height: 100%;
		background: no-repeat 50% 50%;
		background-size: cover;
		-webkit-animation-name: kenburns;
		animation-name: kenburns;
		-webkit-animation-timing-function: linear;
		animation-timing-function: linear;
		-webkit-animation-iteration-count: infinite;
		animation-iteration-count: infinite;
		-webkit-animation-duration: 16s;
		animation-duration: 16s;
		opacity: 1;
		transform: scale(0);
	}

	.slideshow-image:nth-child(1) {
		-webkit-animation-name: kenburns-1;
		animation-name: kenburns-1;
		z-index: 3;
	}

	.slideshow-image:nth-child(2) {
		-webkit-animation-name: kenburns-2;
		animation-name: kenburns-2;
		z-index: 2;
	}

	.slideshow-image:nth-child(3) {
		-webkit-animation-name: kenburns-3;
		animation-name: kenburns-3;
		z-index: 1;
	}

	.slideshow-image:nth-child(4) {
		-webkit-animation-name: kenburns-4;
		animation-name: kenburns-4;
		z-index: 0;
	}

	@-webkit-keyframes kenburns-1 {
		0% {
			opacity: 1;
			transform: scale(1.2);
		}

		1.5625% {
			opacity: 1;
		}

		23.4375% {
			opacity: 1;
		}

		26.5625% {
			opacity: 1;
			transform: scale(1);
		}

		100% {
			opacity: 1;
			transform: scale(1.2);
		}

		98.4375% {
			opacity: 1;
			transform: scale(1.2117647059);
		}

		100% {
			opacity: 1;
		}
	}

	@keyframes kenburns-1 {
		0% {
			opacity: 1;
			transform: scale(1.2);
		}

		1.5625% {
			opacity: 1;
		}

		23.4375% {
			opacity: 1;
		}

		26.5625% {
			opacity: 1;
			transform: scale(1);
		}

		100% {
			opacity: 1;
			transform: scale(1.2);
		}

		98.4375% {
			opacity: 1;
			transform: scale(1.2117647059);
		}

		100% {
			opacity: 1;
		}
	}

	@-webkit-keyframes kenburns-2 {
		23.4375% {
			opacity: 1;
			transform: scale(1.2);
		}

		26.5625% {
			opacity: 1;
		}

		48.4375% {
			opacity: 1;
		}

		51.5625% {
			opacity: 1;
			transform: scale(1);
		}

		100% {
			opacity: 1;
			transform: scale(1.2);
		}
	}

	@keyframes kenburns-2 {
		23.4375% {
			opacity: 1;
			transform: scale(1.2);
		}

		26.5625% {
			opacity: 1;
		}

		48.4375% {
			opacity: 1;
		}

		51.5625% {
			opacity: 1;
			transform: scale(1);
		}

		100% {
			opacity: 0;
			transform: scale(1.2);
		}
	}

	@-webkit-keyframes kenburns-3 {
		48.4375% {
			opacity: 1;
			transform: scale(1.2);
		}

		51.5625% {
			opacity: 1;
		}

		73.4375% {
			opacity: 1;
		}

		76.5625% {
			opacity: 1;
			transform: scale(1);
		}

		100% {
			opacity: 1;
			transform: scale(1.2);
		}
	}

	@keyframes kenburns-3 {
		48.4375% {
			opacity: 1;
			transform: scale(1.2);
		}

		51.5625% {
			opacity: 1;
		}

		73.4375% {
			opacity: 1;
		}

		76.5625% {
			opacity: 1;
			transform: scale(1);
		}

		100% {
			opacity: 1;
			transform: scale(1.2);
		}
	}

	@-webkit-keyframes kenburns-4 {
		73.4375% {
			opacity: 1;
			transform: scale(1.2);
		}

		76.5625% {
			opacity: 1;
		}

		98.4375% {
			opacity: 1;
		}

		100% {
			opacity: 1;
			transform: scale(1);
		}
	}

	@keyframes kenburns-4 {
		73.4375% {
			opacity: 1;
			transform: scale(1.2);
		}

		76.5625% {
			opacity: 1;
		}

		98.4375% {
			opacity: 1;
		}

		100% {
			opacity: 1;
			transform: scale(1);
		}
	}

	/* 相册 end*/
</style>

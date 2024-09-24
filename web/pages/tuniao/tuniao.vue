<template>
	<view class="page-e tn-safe-area-inset-bottom">

		<template v-if="userInfo.userID>0">
			<view class="top-backgroup">
				<image src='http://imagesoss.hunji.xyz/tuniao/my/my-bg4.png' mode='widthFix' class='backgroud-image'>
				</image>
			</view>
		</template>

		<view class="tn-margin-left tn-margin-right" :style="{paddingTop: vuex_custom_bar_height + 'px'}">
			<!-- 图标logo/头像 -->
			<view class="tn-flex tn-flex-row-between tn-flex-col-center tn-margin-bottom" style="margin-top: -450rpx;"
				@click="tn('/pageA/set/set')" v-if="userInfo.userID>0">
				<view class="justify-content-item">
					<view class="tn-flex tn-flex-col-center tn-flex-row-left">
						<view class="logo-pic tn-shadow">
							<view class="logo-image">
								<view class="tn-shadow-blur"
									:style="'background-image:url('+ userInfo.avatarUrl +');width: 110rpx;height: 110rpx;background-size: cover;overflow: hidden;background-position: center center;'">
								</view>
							</view>
						</view>
						<view class="tn-padding-right">
							<view class="tn-padding-right tn-padding-left-sm">
								<text class="tn-color-wallpaper tn-text-xl tn-text-bold">微信用户{{ userInfo.userID}}</text>
								<!-- <text class=" tn-round tn-text-xs tn-bg-red tn-color-white tn-margin-left-sm" style="padding: 10rpx 20rpx;">未实名</text> -->
							</view>
							<view class="tn-padding-right tn-padding-top-xs tn-padding-left-sm tn-text-ellipsis">
								<text class="tn-color-gray">今晚的月色好美</text>
								<!-- <text class="tn-color-gray tn-padding-left-sm tn-text-sm">18637711283</text> -->
							</view>
						</view>
					</view>
				</view>
				<!-- 	<view class="justify-content-item">
					<view class="tn-icon-right tn-color-gray">
					</view>
				</view> -->
			</view>

			<!-- 没有授权，则显示这个授权按钮-->
			<view class="tn-flex tn-flex-row-between" @click="userLogin" v-if="userInfo.userID == undefined">
				<view class="tn-flex-1 justify-content-item tn-margin-xs tn-text-center">
					<tn-button shape="round" backgroundColor="#1D2541" fontColor="#ffffff" padding="20rpx 0" width="40%"
						shadow>
						<text class="tn-icon-wechat tn-padding-right-xs tn-text-xl"></text>
						<text class="">授权登录</text>
					</tn-button>
				</view>
			</view>
			<!-- 邀请好友赚取更多积分 -->
			<!-- <view class="tn-margin-top-xl">
				<view class="button-number tn-flex tn-flex-row-between tn-flex-col-center tn-shadow-blur"
					style="background: linear-gradient(-120deg, #3E445A, #31374A, #2B3042, #262B3C);">

					<view class="tn-margin-left">
						<view class='title' style="color: #F1C68E;">
							<text
								class="tn-text-bold tn-text-xl">{{userInfo.score == undefined ? 0.00 : userInfo.score}}
								积分</text>
							<text class="tn-icon-vip-text tn-text-center"
								style="position: absolute;margin: -22rpx 0 0 0;font-size: 92rpx;"></text>
						</view>
						<view class='tn-color-white tn-text-sm tn-padding-top-sm'>邀请好友赚取更多积分</view>
					</view>
					<view class="tn-margin-right">
						<tn-button shape="round" backgroundColor="#F1C68E" fontColor="#634738" padding="10rpx 0"
							width="160rpx" shadow open-type="share">
							<text class="tn-icon-vip tn-padding-right-sm tn-text-lg"></text>
							<text class="tn-text-bold">邀 请</text>
						</tn-button>
					</view>

				</view>
			</view> -->


			<!-- 方式20 start-->
			<view class="tn-flex">
				<view class="tn-flex-1 wallpaper-shadow tn-bg-white" style="margin: 50rpx 15rpx 0 0;padding: 30rpx 0;"
					@click="tn('/pages/collect/collect')">
					<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
						<view class="icon5__item--icon tn-flex tn-flex-row-center tn-flex-col-center"
							style="background-image:url('http://imagesoss.hunji.xyz/tuniao/my/my9.png');background-size:100% 100%;background-size: cover;">

						</view>
						<view class="tn-text-center" style="font-size: 30rpx;">
							<view class="tn-text-ellipsis">我的收藏</view>
						</view>
					</view>
				</view>
				<view class="tn-flex-1 wallpaper-shadow tn-bg-white" style="margin: 50rpx 0 0 15rpx;padding: 30rpx 0;"
					 @click="copySource" >
					<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
						<view class="icon5__item--icon tn-flex tn-flex-row-center tn-flex-col-center"
							style="background-image:url('http://imagesoss.hunji.xyz/tuniao/my/my17.png');background-size:100% 100%;background-size: cover;">
						</view>
						<view class="tn-text-center" style="font-size: 30rpx;">
							<view class="tn-text-ellipsis">开源地址</view>
						</view>
					</view>
				</view>
			</view>

			<!-- 更多信息-->
			<!-- 方式12 start-->
			<view class="wallpaper-shadow tn-margin-top-lg tn-padding-top-sm tn-padding-bottom-sm tn-bg-white">
				<view class="tn-flex tn-flex-row-center tn-radius tn-padding-top">
					<view class="tn-padding-sm tn-margin-xs tn-radius" @click="tn('/pages/collect/collect')">
						<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
							<view
								class="icon12__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-bg-grey--light">
								<view class="tn-icon-honor tn-color-wallpaper"></view>
							</view>
							<view class="tn-text-center">
								<text class="tn-text-ellipsis">我的收藏</text>
							</view>
						</view>
					</view>
					<view class="tn-padding-sm tn-margin-xs tn-radius" @click="tip()">
						<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
							<view
								class="icon12__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-bg-grey--light">
								<view class="tn-icon-topics tn-color-wallpaper"></view>
							</view>
							<view class="tn-text-center">
								<text class="tn-text-ellipsis">智能助手</text>
							</view>
						</view>
					</view>

					<view class="tn-padding-sm tn-margin-xs tn-radius" @click="tip()">
						<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
							<view
								class="icon12__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-bg-grey--light">
								<view class="tn-icon-help tn-color-wallpaper"></view>
							</view>
							<view class="tn-text-center">
								<text class="tn-text-ellipsis">帮助中心</text>
							</view>
						</view>
					</view>

					<view class="tn-padding-sm tn-margin-xs tn-radius" @click="tip()">
						<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
							<view
								class="icon12__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-bg-grey--light">
								<view class="tn-icon-set tn-color-wallpaper"></view>
							</view>
							<view class="tn-text-center">
								<text class="tn-text-ellipsis">系统设置</text>
							</view>
						</view>
					</view>
				</view>
			</view>


			<!-- 更多信息-->
			<view class="wallpaper-shadow tn-margin-top-lg tn-padding-top-sm tn-padding-bottom-sm">
				<tn-list-cell :hover="true" :unlined="true" :radius="true" :fontSize="30"
					@click="tn('/pages/about/about')">
					<view class="tn-flex tn-flex-col-center">
						<view class="icon1__item--icon tn-flex tn-flex-row-center tn-flex-col-center"
							style="color: #7C8191;">
							<view class="tn-icon-science-fill"></view>
						</view>
						<view class="tn-margin-left-sm tn-flex-1">关于我们</view>
						<view class="tn-color-gray tn-icon-right"></view>
					</view>
				</tn-list-cell>
				<tn-list-cell :hover="true" :unlined="true" :radius="true" :fontSize="30">
					<view class="tn-flex tn-flex-col-center" @click="tip()">
						<view class="icon1__item--icon tn-flex tn-flex-row-center tn-flex-col-center"
							style="color: #7C8191;">
							<view class="tn-icon-light-fill"></view>
						</view>
						<view class="tn-margin-left-sm tn-flex-1">隐私协议</view>
						<view class="tn-color-gray tn-icon-right"></view>
					</view>
				</tn-list-cell>
				<tn-list-cell :hover="true" :unlined="true" :radius="true" :fontSize="30"
					@click="tn('/pages/site/site')">
					<view class="tn-flex tn-flex-col-center">
						<view class="icon1__item--icon tn-flex tn-flex-row-center tn-flex-col-center"
							style="color: #7C8191;">
							<view class="tn-icon-trust-fill"></view>
						</view>
						<view class="tn-margin-left-sm tn-flex-1" >友情链接</view>
						<view class="tn-color-gray tn-icon-right"></view>
					</view>
				</tn-list-cell>
			</view>

			<!-- 底部信息，合作勾搭-问题反馈 -->
			<view class="wallpaper-shadow tn-margin-top-lg tn-margin-bottom-lg tn-padding-top-sm tn-padding-bottom-sm">
				<!-- 			<tn-list-cell :hover="true" :unlined="true" :radius="true" :fontSize="30">
					<button class="tn-flex tn-flex-col-center tn-button--clear-style" open-type="contact">
						<view class="icon1__item--icon tn-flex tn-flex-row-center tn-flex-col-center"
							style="color: #7C8191;">
							<view class="tn-icon-wechat-fill"></view>
						</view>
						<view class="tn-flex tn-flex-row-between" style="width: 100%;">
							<view class="tn-margin-left-sm">合作勾搭</view>
							<view class="tn-color-gray tn-icon-right"></view>
						</view>
					</button>
				</tn-list-cell> -->
				<tn-list-cell :hover="true" :unlined="true" :radius="true" :fontSize="30">
					<button class="tn-flex tn-flex-col-center tn-button--clear-style" open-type="feedback">
						<view class="icon1__item--icon tn-flex tn-flex-row-center tn-flex-col-center"
							style="color: #7C8191;">
							<view class="tn-icon-message-fill"></view>
						</view>
						<view class="tn-flex tn-flex-row-between" style="width: 100%;">
							<view class="tn-margin-left-sm">问题反馈</view>
							<view class="tn-color-gray tn-icon-right"></view>
						</view>
					</button>
				</tn-list-cell>
				<tn-list-cell :hover="true" :unlined="true" :radius="true" :fontSize="30" @click="copySource">
					<view class="tn-flex tn-flex-col-center">
						<view class="icon1__item--icon tn-flex tn-flex-row-center tn-flex-col-center"
							style="color: #7C8191;">
							<view class="tn-icon-wechat-fill"></view>
						</view>
						<view class="tn-margin-left-sm tn-flex-1">技术支持</view>
						<view class="tn-color-gray tn-icon-copy"></view>
					</view>
				</tn-list-cell>
			</view>

		</view>

		<view class='tn-tabbar-height'></view>

	</view>
</template>

<script>
	import {
		userLogin,getUserInfo
	} from '@/utils/api/user'
	export default {
		name: 'Mine',
		data() {
			return {
				userInfo: {},
			}
		},
		//实例已经完成了模板的编译，但是还没有挂载到页面中
		created() {
			this.getUsercaheInfo()
		},
		//跳转携带参数：从A页面跳转到B页面，B页面读取传递过来的参数
		onShow() {
			  this.getUsercaheInfo()
		},
		//监听页面显示。页面每次出现在屏幕上都触发，包括从下级页面点返回露出当前页面。
		onLoad() {
			 this.getUsercaheInfo()
		},
		//此时，已经将编译好的模板，挂载到了页面指定的容器中显示
		mounted() {
			 
		},
		methods: {
			userLogin() {
				uni.navigateTo({
					url: '/pages/login/login'
				})
			},
			getUsercaheInfo() {
				let _that = this
				//判断token是否存在
				var token= uni.getStorageSync("token"); 
				if(token){
					getUserInfo().then(res=>{  
						if(res.code==200){ 
							_that.userInfo = res.body
							uni.setStorageSync("userinfo", res.body)
						}
						 else{
							 this.userwxLogin();
						 }
					})
				} 
				else{
					this.userwxLogin();
				}
				
			},
			userwxLogin(){
				let _that = this
				uni.login({
					provider: 'weixin',
					success(res) {
						userLogin({
							code: res.code
						}).then(res => {
							console.log("userLogin",res);
							uni.setStorageSync("token", res.token)
							uni.setStorageSync("userinfo", res.userinfo)
							_that.userInfo = res.userinfo
							// 登录成功后刷新当前页面 
							console.log("执行啦");
						})
					}
				})
			},
			// 跳转
			tn(e) {
				uni.navigateTo({
					url: e,
				});
			},
			tip(){
				uni.showToast({
					title: '敬请更新',
					icon: "none",
					duration: 3000,
				})
			},
			//拨打固定电话
			callPhoneNumber() {
				uni.makePhoneCall({
					phoneNumber: "186***11283",
				});
			},
			// 复制开源地址
			copySource() {
				wx.vibrateShort();
				uni.setClipboardData({
					data: "https://gitee.com/Pridejoy/wallpaper.net.git",
				})
				uni.showToast({
					title: '复制成功',
					icon: "none",
					duration: 3000,
				})
			},
		}
	}
</script>

<style lang="scss" scoped>
	.page-e {
		max-height: 100vh;
	}

	/* 底部安全边距 start*/
	.tn-tabbar-height {
		min-height: 20rpx;
		height: calc(40rpx + env(safe-area-inset-bottom) / 2);
		height: calc(40rpx + constant(safe-area-inset-bottom));
	}

	.tn-color-wallpaper {
		color: #1D2541;
	}


	/* 自定义导航栏内容 start */
	.custom-nav {
		height: 100%;

		&__back {
			margin: auto 30rpx;
			font-size: 40rpx;
			margin-right: 10rpx;
			flex-basis: 5%;
			width: 100rpx;
			position: absolute;
		}
	}

	/* 自定义导航栏内容 end */

	/* 顶部背景图 start */
	.top-backgroup {
		height: 450rpx;
		z-index: -1;

		.backgroud-image {
			width: 100%;
			height: 450rpx;
			// z-index: -1;
		}
	}

	/* 顶部背景图 end */


	/* 图标容器5 start */
	.icon5 {
		&__item {
			// width: 30%;
			background-color: #FFFFFF;
			border-radius: 10rpx;
			padding: 0rpx;
			margin: 0rpx;
			transform: scale(1);
			transition: transform 0.3s linear;
			transform-origin: center center;

			&--icon {
				width: 96rpx;
				height: 96rpx;
				border-radius: 50%;
				margin-bottom: 18rpx;
				position: relative;
				z-index: 1;
			}
		}
	}

	/* 用户头像 start */
	.logo-image {
		width: 110rpx;
		height: 110rpx;
		position: relative;
		overflow: hidden;
		border-radius: 50%;
	}

	.logo-pic {
		background-size: cover;
		background-repeat: no-repeat;
		// background-attachment:fixed;
		background-position: top;
		border: 8rpx solid rgba(255, 255, 255, 0.05);
		box-shadow: 0rpx 0rpx 80rpx 0rpx rgba(0, 0, 0, 0.15);
		border-radius: 50%;
		overflow: hidden;
		// background-color: #FFFFFF;
	}

	/* 页面阴影 start*/
	.wallpaper-shadow {
		border-radius: 15rpx;
		box-shadow: 0rpx 0rpx 50rpx 0rpx rgba(0, 0, 0, 0.07);
	}

	/* 页面阴影 end*/

	/* 图标容器15 start */
	.icon15 {
		&__item {
			width: 30%;
			background-color: #FFFFFF;
			border-radius: 10rpx;
			padding: 30rpx;
			margin: 20rpx 10rpx;
			transform: scale(1);
			transition: transform 0.3s linear;
			transform-origin: center center;

			&--icon {
				width: 100rpx;
				height: 100rpx;
				font-size: 60rpx;
				border-radius: 50%;
				margin-bottom: 18rpx;
				position: relative;
				z-index: 1;

				&::after {
					content: " ";
					position: absolute;
					z-index: -1;
					width: 100%;
					height: 100%;
					left: 0;
					bottom: 0;
					border-radius: inherit;
					opacity: 1;
					transform: scale(1, 1);
					background-size: 100% 100%;


				}
			}
		}
	}

	/* 图标容器12 start */
	.tn-three {
		position: absolute;
		top: 50%;
		right: 50%;
		bottom: 50%;
		left: 50%;
		transform: translate(-38rpx, -16rpx) rotateX(30deg) rotateY(20deg) rotateZ(-30deg);
		text-shadow: -1rpx 2rpx 0 #f0f0f0, -2rpx 4rpx 0 #f0f0f0, -10rpx 20rpx 30rpx rgba(0, 0, 0, 0.2);
	}

	.icon20 {
		&__item {
			width: 30%;
			background-color: #FFFFFF;
			border-radius: 10rpx;
			padding: 30rpx;
			margin: 20rpx 10rpx;
			transform: scale(1);
			transition: transform 0.3s linear;
			transform-origin: center center;

			&--icon {
				width: 100rpx;
				height: 100rpx;
				font-size: 60rpx;
				border-radius: 50%;
				margin-bottom: 18rpx;
				position: relative;
				z-index: 1;

				&::after {
					content: " ";
					position: absolute;
					z-index: -1;
					width: 100%;
					height: 100%;
					left: 0;
					bottom: 0;
					border-radius: inherit;
					opacity: 1;
					transform: scale(1, 1);
					background-size: 100% 100%;
					background-image: url(http://imagesoss.hunji.xyz/tuniao/cool_bg_image/icon_bg.png);
				}
			}
		}
	}


	/* 积分数字 */
	.button-number {
		width: 100%;
		height: 150rpx;
		border-radius: 15rpx;
		position: relative;
		z-index: 1;

		&::after {
			content: " ";
			position: absolute;
			z-index: -1;
			width: 100%;
			height: 100%;
			left: 0;
			bottom: 0;
			border-radius: inherit;
			opacity: 1;
			transform: scale(1, 1);
			background-size: 100% 100%;
			background-image: url(http://imagesoss.hunji.xyz/tuniao/cool_bg_image/icon_bg.png);
		}
	}


	/* 图标容器12 start */
	.icon12 {
		&__item {
			width: 30%;
			background-color: #FFFFFF;
			border-radius: 10rpx;
			padding: 30rpx;
			margin: 20rpx 10rpx;
			transform: scale(1);
			transition: transform 0.3s linear;
			transform-origin: center center;

			&--icon {
				width: 15rpx;
				height: 15rpx;
				font-size: 50rpx;
				border-radius: 50%;
				margin-bottom: 38rpx;
				position: relative;
				z-index: 1;

				&::after {
					content: " ";
					position: absolute;
					z-index: -1;
					width: 100%;
					height: 100%;
					left: 0;
					bottom: 0;
					border-radius: inherit;
					opacity: 1;
					transform: scale(1, 1);
					background-size: 100% 100%;

				}
			}
		}
	}

	/* 图标容器1 start */
	.icon1 {
		&__item {
			// width: 30%;
			background-color: #FFFFFF;
			border-radius: 10rpx;
			padding: 30rpx;
			margin: 20rpx 10rpx;
			transform: scale(1);
			transition: transform 0.3s linear;
			transform-origin: center center;

			&--icon {
				width: 40rpx;
				height: 40rpx;
				font-size: 40rpx;
				border-radius: 50%;
				position: relative;
				z-index: 1;

				&::after {
					content: " ";
					position: absolute;
					z-index: -1;
					width: 100%;
					height: 100%;
					left: 0;
					bottom: 0;
					border-radius: inherit;
					opacity: 1;
					transform: scale(1, 1);
					background-size: 100% 100%;
					background-image: url(http://imagesoss.hunji.xyz/tuniao/cool_bg_image/icon_bg.png);
				}
			}
		}
	}

	/* 图标容器1 end */
</style>
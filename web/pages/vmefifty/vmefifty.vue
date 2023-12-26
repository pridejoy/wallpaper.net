<template>
	<view class="page-c">

		<!-- 顶部自定义导航 -->
		<!--   <tn-nav-bar :isBack="false" :bottomShadow="false" backgroundColor="none">
      <view class="custom-nav tn-flex tn-flex-col-center tn-flex-row-left" @click="tn('/minePages/message')">
        <view class="custom-nav__back">
          <text class="tn-text-bold tn-text-xl tn-color-black">每日精选</text>
        </view>
      </view>
    </tn-nav-bar> -->
		<tn-nav-bar fixed alpha customBack>
			<view slot="back" class='tn-custom-nav-bar__back' @click="goBack">
				<text class='icon tn-icon-left'></text>
				<text class='icon tn-icon-home-capsule-fill'></text>
			</view>
			<view class="custom-nav__back">
				<text class="tn-text-bold tn-text-xl tn-color-black">精选</text>
			</view>
		</tn-nav-bar>

		<view class="swiper tn-margin-left tn-margin-right" style="height:89vh"
			:style="{paddingTop: vuex_custom_bar_height + 10 +'px'}" @click="imagepreview()">
			<tn-stack-swiper :list="list" direction="vertical" height="105%" :switchRate="20" :scaleRate="0.05"
				:autoplay="true" :translateRate="7.2"></tn-stack-swiper>
		</view>

		<!-- 两个按钮，有需要直接显示出来即可-->
		<!-- 		<view class="tn-footerfixed">
			<view class="tn-flex">
				<view class="tn-flex-1 tn-padding-sm tn-radius justify-content-item" @click="tn('')">
					<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
						<view class="icon4__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-shadow-blur">
							<view class="tn-icon-like-lack tn-cool-color-icon4 tn-bg-blue"></view>
						</view>
						<view class="tn-color-gray--dark tn-text-center">
							<text class="tn-text-ellipsis tn-text-sm">收 藏</text>
						</view>
					</view>
				</view>
				<view class="tn-flex-1 tn-padding-sm tn-radius justify-content-item" @click="tn('')">
					<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
						<view class="icon4__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-shadow-blur">
							<view class="tn-icon-download tn-cool-color-icon4 tn-bg-purplered"></view>
						</view>
						<view class="tn-color-gray--dark tn-text-center">
							<text class="tn-text-ellipsis tn-text-sm">下 载</text>
						</view>
					</view>
				</view>
			</view>
		</view> -->


		<!-- <view class='tn-tabbar-height'></view> -->

	</view>
</template>

<script>
	import {
		getCategImg
	} from '@/utils/api/element.js'
	import template_page_mixin from '@/libs/mixin/template_page_mixin.js'
	export default {
		name: 'PagesC',
		mixins: [template_page_mixin],
		data() {
			return {
				imageUid: '483316112570716083',
				list: [],
				autoplay: false
			}
		},
		onReady() {
			this.$nextTick(() => {
				this.initSwiperContainer()
			})
		},
		onShow() {
			this.autoplay = true
		},
		created() {
			this.getImageList()
		},
		onHide() {
			this.autoplay = true
		},
		methods: {
			getImageList() {
				if (this.list.length <= 5) {
					for (let i = 0; i < 10; i++) {
						getCategImg().then(res => {
							this.list.push({
								image: (res.data.imagesURL)
							})
						})
					}
				} else {
					this.list = [];
					this.getImageList();
				}
			},
			//预览图片
			imagepreview() {
				uni.showToast({
					title: '仅供观赏，自觉删除',
					icon: "none",
					duration: 1000,
				})

				var ls = [];
				this.list.forEach(x => {
					ls.push(x.image)
				})
				// 预览图片 

				uni.previewImage({
					current: 0,
					urls: ls
				});
			},
			// 跳转
			tn(e) {
				uni.navigateTo({
					url: e + "?shareimgid=" + this.imageUid,
				});
			},
			// 初始化轮播图容器
			initSwiperContainer() {
				// 获取底部tabbar信息
				this._tGetRect('.tabbar').then((res) => {
					if (!res.height) {
						setTimeout(() => {
							this.initSwiperContainer()
						}, 10)
						return
					}
					// 获取系统信息
					const systemInfo = uni.getSystemInfoSync()
					this.swiperContainerHeight = systemInfo.safeArea.height - res.height - 10
				})
			}
		}
	}
</script>

<style lang="scss" scoped>
	.page-c {
		max-height: 100vh;
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

	//////
	.template-details {
		margin: 0;
		width: 100%;
		height: 100vh;
		color: #fff;
		overflow: hidden;
	}

	/* 胶囊*/
	.tn-custom-nav-bar__back {
		width: 100%;
		height: 100%;
		position: relative;
		display: flex;
		justify-content: space-evenly;
		align-items: center;
		box-sizing: border-box;
		background-color: rgba(0, 0, 0, 0.15);
		border-radius: 1000rpx;
		border: 1rpx solid rgba(255, 255, 255, 0.5);
		color: #FFFFFF;
		font-size: 18px;

		.icon {
			display: block;
			flex: 1;
			margin: auto;
			text-align: center;
		}

		&:before {
			content: " ";
			width: 1rpx;
			height: 110%;
			position: absolute;
			top: 22.5%;
			left: 0;
			right: 0;
			margin: auto;
			transform: scale(0.5);
			transform-origin: 0 0;
			pointer-events: none;
			box-sizing: border-box;
			opacity: 0.7;
			background-color: #FFFFFF;
		}
	}



	/* 自定义导航栏内容 end */

	/* 底部安全边距 start*/
	.tn-tabbar-height {
		min-height: 120rpx;
		height: calc(140rpx + env(safe-area-inset-bottom) / 2);
		height: calc(140rpx + constant(safe-area-inset-bottom));
	}

	/* 卡片轮播图 start */
	.swiper {
		border-radius: 10rpx;
		overflow: hidden;
	}

	/* 轮播图 end */

	/* 底部固定按钮*/
	.tn-footerfixed {
		position: fixed;
		width: 100%;
		bottom: calc(130rpx + env(safe-area-inset-bottom));
		z-index: 1024;
	}

	/* 图标容器4 start */
	.tn-cool-color-icon4 {
		// background-image: -webkit-linear-gradient(135deg, #ED1C24, #FECE12);
		// background-image: linear-gradient(135deg, #ED1C24, #FECE12);
		-webkit-background-clip: text;
		background-clip: text;
		-webkit-text-fill-color: transparent;
		text-fill-color: transparent;
	}

	.icon4 {
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
				width: 110rpx;
				height: 110rpx;
				font-size: 55rpx;
				border-radius: 50%;
				margin-bottom: 18rpx;
				position: relative;
				z-index: 1;
				background-color: rgba(255, 255, 255, 0.8);
				backdrop-filter: blur(20rpx);
				-webkit-backdrop-filter: blur(20rpx);
				box-shadow: 0rpx 0rpx 30rpx 0rpx rgba(0, 0, 0, 0.07);
			}
		}
	}
</style>
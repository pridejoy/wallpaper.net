<template>
	<view class="index tn-safe-area-inset-bottom">
		<!-- 顶部自定义导航 -->
		<tn-nav-bar fixed :isBack="false" :bottomShadow="false" backgroundColor="#FFFFFF">
			<view class="custom-nav tn-flex tn-flex-col-center tn-flex-row-left">
				<!-- 图标logo -->
				<view class="custom-nav__back" @click="navigateto('')">
					<view class="tn-icon-menu" style="font-size: 50rpx;"></view>
				</view>
				<!-- 搜索框 -->
	<!-- 			<view class="custom-nav__search tn-flex tn-flex-col-center tn-flex-row-center" @click="navigateto('')">
					<view class="custom-nav__search__box tn-flex tn-flex-col-center tn-flex-row-left"
						style="background-color: rgba(230,230,230,0.3);">
						<view class="custom-nav__search__icon tn-icon-search tn-color-gray"></view>
						<view class="custom-nav__search__text tn-padding-left-xs tn-color-gray">搜索 </view>
					</view>
				</view> -->
			</view>
		</tn-nav-bar>

		<view class="tn-flex tn-flex-row-between tn-flex-col-center tn-margin-top"
			:style="{paddingTop: vuex_custom_bar_height + 'px'}">
			<view class="justify-content-item tn-margin-left" @click="navigateto('')">
				<view class="tn-text-bold tn-text-lg tn-padding-bottom-xs">
					Hi 欢迎光临，迷恋图库
				</view>
				<view class="tn-text-xs tn-color-cat" style="opacity: 0.5;">
				   行至朝雾里，坠入暮云间
				</view>
			</view>
			<view class="justify-content-item tn-color-cat tn-text-right">
				<tn-button backgroundColor="#F1C68E00" fontColor="#838383" padding="10rpx 0" width="110rpx" shadow
					open-type="share">
					<text class="tn-icon-wechat-fill tn-text-xl"></text>
				</tn-button>
			</view>
		</view>

 
 

	<view class="">
			<view class="" v-if="current==0">
				<view class="" style="padding: 30rpx 20rpx;">
					<tn-waterfall ref="waterfall" v-model="list" @finish="handleWaterFallFinish">
						<template v-slot:left="{ leftList }">
							<view v-for="(item, index) in leftList" :key="item.waterfallId" class="wallpaper__item"
								@click="imagepreview(item.url)">
								<view class="item__image">
									<tn-lazy-load :threshold="6000" height="100%" :image="item.url" :index="item.id"
										imgMode="widthFix"></tn-lazy-load>
								</view> 
							</view>
						</template>
						<template v-slot:right="{ rightList }">
							<view class="tn-color-black tn-text-bold tn-bg-yellow home-shadow"
								style="height: 160rpx;margin: 0 10rpx 20rpx 10rpx;border-radius: 10rpx;">
								<view class="tn-padding-left tn-padding-top-lg">
									Random ·Girl
								</view>
								<view class="tn-padding-left tn-padding-top-xs">
									Photos 10000+
									<text class="tn-icon-right tn-padding-left-xs"></text>
								</view>
							</view>
							<view v-for="(item, index) in rightList" :key="item.waterfallId" class="wallpaper__item">
								<view class="item__image" @click="imagepreview(item.url)">
									<tn-lazy-load :threshold="6000" height="100%" :image="item.url" :index="item.id"
										imgMode="widthFix"></tn-lazy-load>
								</view>
							</view>
						</template>
					</tn-waterfall>
				</view>
				<tn-load-more :status="loadStatus"></tn-load-more>
			</view>


		</view>

	 
 
		<!-- <view class='tn-tabbar-height'></view> -->
		<!-- 回到首页悬浮按钮-->
		<!-- <nav-index-button></nav-index-button> -->
	</view>
</template>

<script>
	// import {
	// 	getArticlePage
	// } from '@/api/blog.js';
	import NavIndexButton from '@/libs/components/nav-index-button.vue'
	export default {
		name: 'Index',
		components: {
			NavIndexButton
		},
		data() {
			return {
				// 分页请求参数
				param: {
					page: 1,
					pageSize: 15
				} 
			}
		},
		created() {
			this.getarticlepage();
		},
		methods: {
			/**
			 * 获取文章列表
			 * */
			getarticlepage() {
				// getArticlePage(this.param).then(res => {
				// 	if (res.data.success && res.data.code == 200) {
				// 		this.content.push(...res.data.data.rows)
				// 		// this.status = 'loadmore'
				// 	} else {
				// 		console.log("获取数据失败了")
				// 	}
				// }).catch(resolve => {
				// 	console.log("获取数据异常了", resolve)
				// })
			},
			// cardSwiper
			cardSwiper(e) {
				this.cardCur = e.detail.current
			},
			// 跳转
			navigateto(e) {
				uni.navigateTo({
					url: e,
				});
			},
		},
		/**监听滑动到底部执行*/
		onReachBottom() {
			this.param.page++;
			console.log("当前页码：" + this.param.page)
			this.getarticlepage();

		}
	}
</script>

<style lang="scss" scoped>
	.image-pic{
		max-width: 690rpx;
		min-width: 690rpx;
		max-height: 350rpx;
		min-height: 350rpx;
	}
	
	
	.index {
		max-height: 100vh;
	}

	/* 底部安全边距 start*/
	.tn-tabbar-height {
		min-height: 120rpx;
		height: calc(140rpx + env(safe-area-inset-bottom) / 2);
		height: calc(140rpx + constant(safe-area-inset-bottom));
	}

	/* 轮播视觉差 start */
	.card-swiper {
		height: 330rpx !important;
	}

	.card-swiper swiper-item {
		width: 750rpx !important;
		left: 0rpx;
		box-sizing: border-box;
		padding: 40rpx 30rpx 30rpx 30rpx;
		overflow: initial;
	}

	.card-swiper swiper-item .swiper-item {
		width: 100%;
		display: block;
		height: 100%;
		border-radius: 15rpx;
		transform: scale(1);
		transition: all 0.2s ease-in 0s;
		// overflow: hidden;
	}

	.card-swiper swiper-item.cur .swiper-item {
		transform: none;
		transition: all 0.2s ease-in 0s;
	}

	.card-swiper swiper-item .swiper-item-text {
		margin-top: -160rpx;
		text-align: center;
		width: 100%;
		display: block;
		height: 50%;
		border-radius: 10rpx;
		transform: translate(100rpx, -60rpx) scale(0.9, 0.9);
		transition: all 0.6s ease 0s;
		overflow: hidden;
	}

	.card-swiper swiper-item.cur .swiper-item-text {
		margin-top: -220rpx;
		width: 100%;
		transform: translate(0rpx, 0rpx) scale(0.9, 0.9);
		transition: all 0.6s ease 0s;
	}

	.image-banner {
		border: 1rpx solid #F8F7F8;
		display: flex;
		align-items: center;
		justify-content: center;
	}

	.image-banner image {
		width: 100%;
		height: 100%;
	}

	/* 轮播指示点 start*/
	.indication {
		z-index: 9999;
		width: 100%;
		height: 36rpx;
		position: absolute;
		display: flex;
		flex-direction: row;
		align-items: center;
		justify-content: center;
	}

	.spot {
		background-color: #FFFFFF;
		opacity: 0.6;
		width: 10rpx;
		height: 10rpx;
		border-radius: 20rpx;
		top: -70rpx;
		margin: 0 8rpx !important;
		position: relative;
	}

	.spot.active {
		opacity: 1;
		width: 30rpx;
		background-color: #FFFFFF;
	}



	/* 图标容器12 start */
	.tn-three {
		position: absolute;
		top: 50%;
		right: 50%;
		bottom: 50%;
		left: 50%;
		transform: translate(-38rpx, -20rpx) rotateX(20deg) rotateY(10deg) rotateZ(-20deg);
		text-shadow: -1rpx 2rpx 0 #f0f0f0, -2rpx 4rpx 0 #f0f0f0, -10rpx 20rpx 30rpx rgba(0, 0, 0, 0.2);
	}

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
					background-image: url(https://tnuiimage.tnkjapp.com/cool_bg_image/icon_bg6.png);


				}
			}
		}
	}

	/* 自定义导航栏内容 start */
	.custom-nav {
		height: 100%;

		&__back {
			margin: auto 5rpx;
			font-size: 40rpx;
			margin-right: 10rpx;
			margin-left: 30rpx;
			flex-basis: 5%;
		}

		&__search {
			flex-basis: 60%;
			width: 100%;
			height: 100%;

			&__box {
				width: 100%;
				height: 70%;
				padding: 10rpx 0;
				margin: 0 30rpx;
				border-radius: 60rpx 60rpx 0 60rpx;
				font-size: 24rpx;
			}

			&__icon {
				padding-right: 10rpx;
				margin-left: 20rpx;
				font-size: 30rpx;
			}

			&__text {
				// color: #AAAAAA;
			}
		}
	}

	.logo-image {
		width: 65rpx;
		height: 65rpx;
		position: relative;
		border: 1rpx solid #F8F7F8;
		border-radius: 50%;
	}

	.logo-pic {
		background-size: cover;
		background-repeat: no-repeat;
		// background-attachment:fixed;
		background-position: top;
		border-radius: 50%;
	}

	/* 自定义导航栏内容 end */


	/* 热门图片 start*/
	.image-tuniao1 {
		padding: 164rpx 0rpx;
		font-size: 40rpx;
		font-weight: 300;
		position: relative;
	}

	.image-tuniao2 {
		padding: 75rpx 0rpx;
		font-size: 40rpx;
		font-weight: 300;
		position: relative;
	}

	.image-tuniao3 {
		padding: 90rpx 0rpx;
		font-size: 40rpx;
		font-weight: 300;
		position: relative;
	}

	.image-pic {
		border: 1rpx solid #F8F7F8;
		background-size: cover;
		background-repeat: no-repeat;
		// background-attachment:fixed;
		background-position: top;
		border-radius: 10rpx;
	}

	/* 文章内容 start*/
	.tn-blogger-content {
		&__wrap {
			margin: 30rpx;
		}

		&__info {
			&__btn {
				margin-right: -12rpx;
				opacity: 0.5;
			}
		}

		&__label {
			&__item {
				color: #1D2541;
				background-color: #F3F2F7;
				border-radius: 10rpx;
				font-size: 22rpx;

				padding: 5rpx 15rpx;
				margin: 5rpx 0 0 18rpx;

				&--prefix {
					font-size: 24rpx;
					color: #1D2541;
					padding-right: 10rpx;
				}
			}

			&__desc {
				line-height: 55rpx;
			}
		}

		&__main-image {
			border: 1rpx solid #F8F7F8;
			border-radius: 16rpx;

			&--1 {
				max-width: 690rpx;
				min-width: 690rpx;
				max-height: 400rpx;
				min-height: 400rpx;
			}

			&--2 {
				max-width: 260rpx;
				max-height: 260rpx;
			}

			&--3 {
				height: 212rpx;
				width: 100%;
			}
		}

		&__count-icon {
			font-size: 40rpx;
			padding-right: 5rpx;
		}
	}

	.image-wallpaper {
		padding: 160rpx 0rpx;
		font-size: 40rpx;
		font-weight: 300;
		position: relative;
	}

	.image-pic {
		background-size: cover;
		background-repeat: no-repeat;
		// background-attachment:fixed;
		background-position: top;
		border-radius: 10rpx;
	}

	/* 文章内容 end*/
</style>

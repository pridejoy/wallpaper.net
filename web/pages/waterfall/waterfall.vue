<template>
	<view class="pages-a">
		<!-- 顶部自定义导航 -->
		<tn-nav-bar fixed alpha customBack>
			<view slot="back" class='tn-custom-nav-bar__back' @click="goBack">
				<text class='icon tn-icon-left'></text>
				<text class='icon tn-icon-home-capsule-fill'></text>
			</view>
		</tn-nav-bar>

		<view class="" :style="{paddingTop: vuex_custom_bar_height + 'px'}">
			<tn-tabs :list="scrollList" :current="current" @change="tabChange" activeColor="#000" :bold="true"
				:fontSize="36"></tn-tabs>
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

	</view>
</template>

<script>
	import {
		getWaterfall
	} from '@/utils/api/tools.js';

	import template_page_mixin from '@/libs/mixin/template_page_mixin.js'
	export default {
		name: 'PagesA',
		mixins: [template_page_mixin],
		data() {
			return {
				// 分页请求参数
				param: {
					page: 1,
					pageSize: 15
				},
				cardCur: 0,
				isAndroid: true,
				current: 0,
				scrollList: [{
						name: '推荐'
					},
					// {
					// 	name: '最新'
					// },
					// {
					// 	name: '热门'
					// }
				],

				rightList: [],
				/* 瀑布流*/
				loadStatus: 'loadmore',
				list: [],
				data: [] //存放接口里面请求的数据
			}
		},
		created() {
			const systemInfo = uni.getSystemInfoSync()
			if (systemInfo.system.toLocaleLowerCase().includes('ios')) {
				this.isAndroid = false
			} else {
				this.isAndroid = true
			}
			/* 瀑布流*/
			this.getRandomData()
		},
		onReachBottom() {
			// this.param.page++;
			// console.log("执行到底了");
			// /* 瀑布流*/
			// this.getRandomData() 

		},
		methods: {
			// cardSwiper
			cardSwiper(e) {
				this.cardCur = e.detail.current
			},

			// tab选项卡切换
			tabChange(index) {
				this.current = index
			},

			// 跳转
			goinfo(item) {
				debugger
				uni.navigateTo({
					url: '/pages/randomgril/randomgril?shareimgid' + item.waterfallId,
				});
			},
			//预览图片
			imagepreview(url) {
				uni.showToast({
					title: '仅供观赏，自觉删除',
					icon: "none",
					duration: 3000,
				})
				// 预览图片 
				uni.previewImage({
					current: 0,
					urls: [url]
				});
			},
			/* 瀑布流*/
			// 获取随机数据
			getRandomData() {
				this.loadStatus = 'loading'
				getWaterfall(this.param).then(res => {
					if (res.success && res.code == 200) {
						//数据加载完
						if (res.data.rows.count == 0) {
							this.loadStatus = 'loadmore'
						}
						this.list.push(...res.data.rows);
					} else {
						console.log("获取数据失败了")
					}
				}).catch(resolve => {
					console.log("获取数据异常了", resolve)
				})

			},
			// 瀑布流加载完毕事件
			handleWaterFallFinish() {
				this.param.page++;
				console.log("瀑布流加载完毕事件");
				/* 瀑布流*/
				this.getRandomData()
				this.loadStatus = 'loadmore'
			}
		}
	}
</script>

<style lang="scss" scoped>
	@import '@/static/css/templatePage/custom_nav_bar.scss';

	.pages-a {
		max-height: 100vh;
	}


	/* 自定义导航栏内容 start */
	.custom-nav {
		height: 100%;

		&__back {
			margin: auto 5rpx;
			font-size: 50rpx;
			margin-right: 10rpx;
			margin-left: 30rpx;
			flex-basis: 5%;
		}
	}

	/* 底部安全边距 start*/
	.tn-tabbar-height {
		min-height: 120rpx;
		height: calc(140rpx + env(safe-area-inset-bottom) / 2);
		height: calc(140rpx + constant(safe-area-inset-bottom));
	}

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
		will-change: transform;
		// overflow: hidden;
	}

	.card-swiper swiper-item.cur .swiper-item {
		transform: none;
		transition: all 0.2s ease-in 0s;
		will-change: transform;
	}

	.card-swiper swiper-item .swiper-item-text {
		margin-top: -220rpx;
		text-align: center;
		width: 100%;
		display: block;
		border-radius: 10rpx;
		transform: translate(100rpx, 0rpx) scale(0.9, 0.9);
		transition: all 0.6s ease 0s;
		will-change: transform;
		overflow: hidden;
	}

	.card-swiper swiper-item.cur .swiper-item-text {
		margin-top: -220rpx;
		width: 100%;
		transform: translate(0rpx, 0rpx) scale(0.9, 0.9);
		transition: all 0.6s ease 0s;
		will-change: transform;
	}

	.image-banner {
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


	/* 胶囊banner*/
	.image-capsule {
		padding: 100rpx 0rpx;
		font-size: 40rpx;
		font-weight: 300;
		position: relative;
	}

	.image-piccapsule {
		background-size: cover;
		background-repeat: no-repeat;
		// background-attachment:fixed;
		background-position: top;
		border-radius: 20rpx 20rpx 0 0;
	}

	/* 用户头像 start */
	.logo-image {
		width: 40rpx;
		height: 40rpx;
		position: relative;
	}

	.logo-pic {
		background-size: cover;
		background-repeat: no-repeat;
		// background-attachment:fixed;
		background-position: top;
		border: 1rpx solid rgba(255, 255, 255, 0.05);
		// box-shadow: 0rpx 0rpx 80rpx 0rpx rgba(0, 0, 0, 0.15);
		border-radius: 50%;
		overflow: hidden;
		// background-color: #FFFFFF;
	}

	/* 瀑布流*/
	.wallpaper__item {
		background-color: #FFFFFF;
		overflow: hidden;
		margin: 0 10rpx;
		margin-bottom: 40rpx;
		// box-shadow: 0rpx 0rpx 30rpx 0rpx rgba(0, 0, 0, 0.07);

		.item {

			/* 图片 start */
			&__image {
				width: 100%;
				height: auto;
				background-color: #FFFFFF;
				border: 1rpx solid #F8F7F8;
				border-radius: 10rpx;
				overflow: hidden;
			}

			/* 图片 end */

			/* 内容 start */
			&__data {
				padding: 14rpx 0rpx;
			}

			/* 标题 start */
			&__title-container {
				text-align: justify;
				line-height: 38rpx;
				vertical-align: middle;
			}

			&__title {
				font-size: 30rpx;
			}

			/* 标题 end */

			/* 标签 start */
			&__tags-container {
				display: flex;
				flex-direction: row;
				flex-wrap: nowrap;
				align-items: center;
				justify-content: flex-start;
			}

			&__tag {
				margin: 10rpx;
				color: #7C8191;
				background-color: #F3F2F7;
				padding: 4rpx 14rpx 6rpx;
				border-radius: 10rpx;
				font-size: 20rpx;

				&:first-child {
					margin-left: 0rpx !important;
				}
			}

			/* 标签 end */

			/* 内容 end */
		}
	}
</style>
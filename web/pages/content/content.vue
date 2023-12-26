<template>
	<view class="template-design tn-safe-area-inset-bottom">


 		<tn-nav-bar fixed>首页</tn-nav-bar>
		<!-- 页面内容 -->
		<view :style="{paddingTop: vuex_custom_bar_height + 'px'}">
<!-- 			<view class="justify-content-item tn-margin-left" @click="navigateto('')">
				<view class="tn-text-bold tn-text-lg tn-padding-bottom-xs">
					Hi 早安，抓住那只猪
				</view>
				<view class="tn-text-xs tn-color-cat" style="opacity: 0.5;">
					清晨的毒鸡汤，已为你备好，趁热喝了叭
				</view>
			</view>
			<view class="justify-content-item tn-color-cat tn-text-right">
				<tn-button backgroundColor="#F1C68E00" fontColor="#838383" padding="10rpx 0" width="110rpx" shadow
					open-type="share">
					<text class="tn-icon-wechat-fill tn-text-xl"></text>
				</tn-button>
			</view> -->
			<view class="margin-top:20px;">
				<tn-swiper :list="banner" v-if="banner.length>0" :height="400" :effect3d="false" mode="number"></tn-swiper>
			</view>

		</view>

		<!-- 方式10 start-->
		<view class="tn-flex tn-margin-top">
			<view class="tn-flex-1 tn-padding-sm tn-margin-xs tn-radius">
				<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
					<view
						class="icon10__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-shadow-blur tn-bg-blue tn-color-white">
						<view class="tn-icon-image-fill"></view>
					</view>
					<view class="tn-color-black tn-text-lg tn-text-center">
						<text class="tn-text-ellipsis">相册</text>
					</view>
				</view>
			</view>
			<view class="tn-flex-1 tn-padding-sm tn-margin-xs tn-radius">
				<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
					<view
						class="icon10__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-shadow-blur tn-bg-red tn-color-white">
						<view class="tn-icon-live-stream-fill"></view>
					</view>
					<view class="tn-color-black tn-text-lg tn-text-center">
						<text class="tn-text-ellipsis">分类</text>
					</view>
				</view>
			</view>
			<view class="tn-flex-1 tn-padding-sm tn-margin-xs tn-radius">
				<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
					<view
						class="icon10__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-shadow-blur tn-bg-orange tn-color-white">
						<view class="tn-icon-image-text-fill"></view>
					</view>
					<view class="tn-color-black tn-text-lg tn-text-center">
						<text class="tn-text-ellipsis">标签</text>
					</view>
				</view>
			</view>
			<view class="tn-flex-1 tn-padding-sm tn-margin-xs tn-radius">
				<view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
					<view
						class="icon10__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-shadow-blur tn-bg-purple tn-color-white">
						<view class="tn-icon-topics-fill"></view>
					</view>
					<view class="tn-color-black tn-text-lg tn-text-center">
						<text class="tn-text-ellipsis">归档</text>
					</view>
				</view>
			</view>
			<view class="tn-flex-1 tn-padding-sm tn-margin-xs tn-radius">
        <view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
          <view class="icon10__item--icon tn-flex tn-flex-row-center tn-flex-col-center tn-shadow-blur tn-bg-cyan tn-color-white">
            <view class="tn-icon-discover-fill"></view>
          </view>  
          <view class="tn-color-black tn-text-lg tn-text-center">
            <text class="tn-text-ellipsis">搜索</text>
          </view>
        </view>
      </view>
		</view>
		<!-- 方式10 end-->


		<!-- 图文 -->
		<view class="tn-flex tn-flex-direction-column">
			<block v-for="(item,index) in content" :key="index" >
				<view class="tn-blogger-content__wrap" @click="gotoContentinfo(item.articleId)">
					<view class="tn-padding-top-xs">
						<!-- 方式一 -->
						<view class="tn-shadow-blur image-pic"
							:style="'background-image:url(' + item.articleCover + ')'">
							<view class="image-design">
							</view>
						</view> 
					</view>
					<view class="tn-blogger-content__label tn-text-justify tn-margin-top tn-margin-bottom-sm">
						<text >{{ item.title }}</text>
					</view>

					<view class="tn-flex tn-flex-row-between tn-flex-col-center tn-margin-top-xs">
						<view class="justify-content-item tn-flex tn-flex-col-center">
							<view style="margin-right: 10rpx;margin-left: 0rpx;">
								<view class="tn-color-gray">
									
									<text class="tn-blogger-content__count-icon tn-icon-like"></text>
									<text class="tn-padding-right">99</text> 
									<text class="tn-blogger-content__count-icon tn-icon-time"></text>
									<text class="tn-padding-right">{{ item.createdTime }}</text>
									<!-- <text class="tn-blogger-content__count-icon tn-icon-message"></text>
									<text class="tn-padding-right">{{ item.commentCount }}</text>-->
								</view>
							</view>
						</view>
						<view class="justify-content-item tn-text-center">
							<view v-for="(label_item,label_index) in item.label" :key="label_index"
								class="tn-blogger-content__label__item tn-float-left tn-margin-right tn-bg-gray--light tn-round tn-text-sm tn-text-bold">
								<text class="tn-blogger-content__label__item--prefix">#</text> {{ label_item }}
							</view>

						</view>
					</view>
				</view>

				<!-- 边距间隔 -->
				<view class="tn-strip-bottom" v-if="index != content.length - 1"></view>
			</block>
		</view>



		<!-- 回到首页悬浮按钮-->
		<nav-index-button></nav-index-button>


	</view>
</template>

<script>
	import template_page_mixin from '@/libs/mixin/template_page_mixin.js'
	import NavIndexButton from '@/libs/components/nav-index-button.vue'
	// import {
	// 	getArticlePage
	// } from '@/api/blog.js';
	export default {
		name: 'TemplateDesign',
		mixins: [template_page_mixin],
		components: {
			NavIndexButton
		},
		data() {
			return {
				// 分页请求参数
				param: {
					page: 1,
					pageSize: 15
				},
				banner: [{
						image: "https://cn.bing.com/th?id=OHR.LoonyDook_ZH-CN1879420705_1920x1080.jpg"
					},
					{
						image: "https://cn.bing.com/th?id=OHR.LargestCave_ZH-CN2069899703_1920x1080.jpg"
					},
				],
				content: [

				]
			}
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
			gotoContentinfo(articleId){				
				uni.navigateTo({
					url: '../content/contentinfo?articleId='+articleId,
				})
			}
		
		},
		created() { 
			this.getarticlepage();
		},
		/**监听滑动到底部执行*/
		onReachBottom() {
			this.param.page++;
			console.log("当前页码："+this.param.page)
			this.getarticlepage();

		}
	}
</script>

<style lang="scss" scoped>
	@import '@/static/css/templatePage/custom_nav_bar.scss';

	.template-design {}

	/* 图标容器10 start */
	.icon10 {
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
				width: 84rpx;
				height: 65rpx;
				font-size: 45rpx;
				border-radius: 200rpx;
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
					background-image: url(http://imagesoss.hunji.xyz/tuniao/cool_bg_image/icon_bg6.png);
				}
			}
		}
	}

	/* 图标容器10 end */

	/* 文章内容 start*/
	.tn-blogger-content {
		&__wrap {
			padding: 30rpx;
		}

		&__info {
			&__btn {
				margin-right: -12rpx;
				opacity: 0.5;
			}
		}

		&__label {
			&__item {
				line-height: 45rpx;
				padding: 0 20rpx;
				margin: 5rpx 18rpx 0 0;

				&--prefix {
					color: #00FFC8;
					padding-right: 10rpx;
				}
			}

			&__desc {
				line-height: 55rpx;
			}
		}

		&__main-image {
			box-shadow: 0rpx 5rpx 40rpx 0rpx rgba(43, 158, 246, 0.2);
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

	.image-design {
		padding: 180rpx 0rpx;
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

	/* 间隔线 start*/
	.tn-strip-bottom {
		width: 100%;
		border-bottom: 20rpx solid rgba(241, 241, 241, 0.3);
	}

	/* 间隔线 end*/

	/* 底部tabbar start*/
	.footerfixed {
		position: fixed;
		width: 100%;
		bottom: 0;
		z-index: 999;
		background-color: #FFFFFF;
		box-shadow: 0rpx 0rpx 30rpx 0rpx rgba(0, 0, 0, 0.07);
	}

	.tabbar {
		display: flex;
		align-items: center;
		min-height: 110rpx;
		justify-content: space-between;
		padding: 0;
		height: calc(110rpx + env(safe-area-inset-bottom) / 2);
		padding-bottom: calc(env(safe-area-inset-bottom) / 2);
	}

	.tabbar .action {
		font-size: 22rpx;
		position: relative;
		flex: 1;
		text-align: center;
		padding: 0;
		display: block;
		height: auto;
		line-height: 1;
		margin: 0;
		overflow: initial;
	}

	.tabbar .action .bar-icon {
		width: 100rpx;
		position: relative;
		display: block;
		height: auto;
		margin: 0 auto 10rpx;
		text-align: center;
		font-size: 42rpx;
	}

	.tabbar .action .bar-icon image {
		width: 50rpx;
		height: 50rpx;
		display: inline-block;
	}
</style>

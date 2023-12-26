<template>
	<view class="page-b"  >

		<!-- 顶部自定义导航 -->
		<!--<tn-nav-bar :isBack="true" :bottomShadow="false" backgroundColor="none">
      <view class="custom-nav tn-flex tn-flex-col-center tn-flex-row-left">
        <view class="custom-nav__back">
          <text class="tn-text-bold tn-text-xl tn-color-black">必硬壁纸</text>
        </view>
      </view>
    </tn-nav-bar> -->
	
		<tn-nav-bar fixed alpha customBack>
			<view slot="back" class='tn-custom-nav-bar__back' @click="goBack">
				<text class='icon tn-icon-left'></text>
				<text class='icon tn-icon-home-capsule-fill'></text>
			</view>
			<view class="custom-nav__back">
				<text class="tn-text-bold tn-text-xl tn-color-black">每日必硬</text>
			</view>
		</tn-nav-bar>

		<view class="" :style="{paddingTop: vuex_custom_bar_height + 'px'}">

			<!-- 搜索框 -->
			<view class="tn-classify__search--wrap" style="padding-bottom: 20rpx;">
				<!--        <view class="tn-color-gray--dark" style="margin: 20rpx 30rpx 0rpx 30rpx;border-radius: 100rpx;padding-left: 6rpx;background-color: rgba(248, 247, 248, 0.9);" @click="tn('/pageA/search/search')">
         <tn-notice-bar :list="searlist" mode="vertical" leftIconName="search" :duration="6000"></tn-notice-bar>
        </view> -->
			</view>

			<!-- 分类列表和内容 -->
			<view class="tn-classify__container">
				<view class="tn-classify__container__wrap tn-flex tn-flex-nowrap tn-flex-row-around tn-bg-white">
					<!-- 左边容器 -->
					<scroll-view class="tn-classify__left-box left-width" :scroll-top="leftScrollViewTop" scroll-y
						scroll-with-animation :style="{height: scrollViewHeight + 'px'}">
						<view v-for="(item, index) in tabbar" :key="index" :id="`tabbar_item_${index}`"
							class="tn-classify__tabbar__item tn-flex tn-flex-col-center tn-flex-row-center"
							:class="[tabbarItemClass(index)]" @tap.stop="clickClassifyNav(index)">
							<view class="tn-classify__tabbar__item__title">{{ item }}</view>
						</view>
					</scroll-view>

					<!-- 右边容器 -->
					<scroll-view class="tn-classify__right-box" scroll-y :scroll-top="rightScrollViewTop"
						:style="{height: scrollViewHeight + 'px'}">
						<block v-if="classdetails && classdetails.length > 0">
							<view class="tn-classify__content">

								<!-- 推荐壁纸轮播图，有需要直接显示出来即可 -->
								<!--<view class="tn-classify__content__recomm">
                  <tn-swiper v-if="classifyContent.recommendProduct.length > 0" class="tn-classify__content__recomm__swiper" :list="classifyContent.recommendProduct" :height="100" :effect3d="true" mode=""></tn-swiper>
                </view> -->

								<!-- 分类内容子栏目 -->
								<view class="tn-classify__content__sub-classify">
									<view
										class="tn-classify__content__sub-classify--title tn-text-lg tn-padding-top-sm">
										{{currentstr}}</view>
									<view
										class="tn-classify__content__sub-classify__content tn-flex tn-flex-wrap tn-flex-col-center tn-flex-row-left">
										<view v-for="(item,index) in classdetails" :key="index"
											class="tn-classify__content__sub-classify__content__item tn-flex tn-flex-wrap tn-flex-row-center">
											<view
												class="tn-classify__content__sub-classify__content__image tn-flex tn-flex-col-center tn-flex-row-center"
												@click="imagepreview(item.mobileUrl)">
												<image :src="item.mobileUrl" mode="aspectFill"></image>
											</view>
											<!-- 标题，有需要可以显示出来 -->
											<view
												class="tn-classify__content__sub-classify__content__title tn-margin-top-xs tn-margin-bottom-sm">
												{{ item.startDate|Fdate }}</view>
										</view>
									</view>
								</view>
							</view>

						</block>
					</scroll-view>
				</view>
			</view>
		</view>

		<!-- 回到首页悬浮按钮-->
		<!-- <nav-index-button></nav-index-button> -->
	</view>
</template>

<script>
	import {
		everyDayBing
	} from '@/utils/api/element.js';
	import NavIndexButton from '@/libs/components/nav-index-button.vue' 
	import template_page_mixin from '@/libs/mixin/template_page_mixin.js'
	export default {
		components: {
			NavIndexButton
		},
		mixins: [template_page_mixin],
		data() {
			return {
				// 侧边栏分类数据
				tabbar: [

				],
				// 分类里面的内容信息【无用】
				classifyContent: {
					// 子栏目
					subClassifyTabbar: [],
					// 每个子栏目下的内容
					subClassify: [],
				},
				//存放当前分类的数据
				classdetails: [{
					"isDeleted": false,
					"id": 584,
					"copyRight": "达恩附近普法尔茨森林中的Altdahn城堡，德国莱茵兰-普法尔茨(Dahn Rockland), Palatinate Forest, Rhineland-Palatinate, Germany (© Reinhard Schmid/Huber/eStock Photo)",
					"gitUrl": null,
					"startDate": "2020-05-01 00:00:00",
					"url": "https://cn.bing.com/th?id=OHR.BurgAltdahn_ZH-CN8281669977_1920x1080.jpg",
					"mobileUrl": "https://cn.bing.com/th?id=OHR.BurgAltdahn_ZH-CN8281669977_1080x1920.jpg"
				}, ],
				currentstr: "",
				// 分类菜单item的信息
				tabbarItemInfo: [],
				// scrollView的top值
				scrollViewBasicTop: 0,
				// scrollView的高度
				scrollViewHeight: 0,
				// 左边scrollView的滚动高度
				leftScrollViewTop: 0,
				// 右边scrollView的滚动高度
				rightScrollViewTop: 0,
				// 当前选中的tabbar序号
				currentTabbarIndex: 0
			}
		},
		created() {
			//获取系统支持的所有年月
			this.getlastYearMonth();
		},
		computed: {
			tabbarItemClass() {
				return index => {
					if (index === this.currentTabbarIndex) {
						return 'tn-classify__tabbar__item--active tn-bg-white'
					} else {
						let clazz = 'tn-bg-gray--light'
						if (this.currentTabbarIndex > 0 && index === this.currentTabbarIndex - 1) {
							clazz += ' tn-classify__tabbar__item--active--prev'
						}
						if (this.currentTabbarIndex < this.tabbar.length && index === this.currentTabbarIndex + 1) {
							clazz += ' tn-classify__tabbar__item--active--next'
						}
						return clazz
					}
				}
			}
		},
		mounted() {
			this.$nextTick(() => {
				this.getScrollViewInfo()
				this.getTabbarItemRect()
			})
		},
		methods: {
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
			//获取当日必应
			getBingData(str) {
				everyDayBing({
					monthstr: str
				}).then(res => {
				
					if (res.success && res.code == 200) {
						// console.log("获取数据每个月", res.data.data)
						this.classdetails = res.data;
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
			},
			// 获取
			getlastYearMonth() {
				// 获取当前日期
				var currentDate = new Date();
				//设置标题为当前的时间
				this.currentstr = this.$options.filters['Fdatetwo'](currentDate);
				//重新获取当前月份的数据
				this.getBingData(this.currentstr);
				// 指定开始日期为2018年9月
				var startDate = new Date(2018, 8);
				// 用一个数组来存储月份列表
				//var  monthList = []; 
				// 循环从开始日期到当前日期的每个月
				for (let d = startDate; d <= currentDate; d.setMonth(d.getMonth() + 1)) {
					var mon = (d.getMonth() + 1).toString().padStart(2, '0');
					// 将当前月份添加到数组中
					this.tabbar.unshift(`${d.getFullYear()}-${mon}`);
				}
				// 打印月份列表
				// console.log(monthList.reverse());					     
			},
			// 跳转
			tn(e, str) {
				//获取当前的数组 
				var info = this.classdetails.filter((x) => {
					return x.id === str
				});

				uni.navigateTo({
					url: e,
				}); 

			}, 
			// 获取scrollView的高度信息
			getScrollViewInfo() {
				// 获取搜索栏的bottom信息，然后用整个屏幕的可用高度减去bottom的值即可获取scrollView的高度(防止双重滚动)
				this._tGetRect('.tn-classify__search--wrap').then((rect) => {
					// 如果获取失败重新获取
					if (!rect) {
						setTimeout(() => {
							this.getScrollViewInfo()
						}, 10)
						return
					}
					// 获取当前屏幕的可用高度
					const systemInfo = uni.getSystemInfoSync()
					this.scrollViewBasicTop = systemInfo.statusBarHeight + rect.bottom + uni.upx2px(10)
					this.scrollViewHeight = systemInfo.safeArea.height + systemInfo.statusBarHeight - rect.bottom -
						uni.upx2px(10)
				})
			},
			// 获取分类菜单每个item的信息
			getTabbarItemRect() {

				let view = uni.createSelectorQuery().in(this)
				for (let i = 0; i < this.tabbar.length; i++) {
					view.select('#tabbar_item_' + i).boundingClientRect()
				}
				view.exec(res => {
					if (!res.length) {
						setTimeout(() => {
							this.getTabbarItemRect()
						}, 10)
						return
					}

					// 将每个分类item的相关信息
					res.map((item) => {
						this.tabbarItemInfo.push({
							top: item.top,
							height: item.height
						})
					})
				})
			},
			// 点击了分类导航
			clickClassifyNav(index) {
				if (this.currentTabbarIndex === index) {
					return
				}
				this.currentTabbarIndex = index
				this.handleLeftScrollView(index)
				this.switchClassifyContent()
			},
			// 点击分类后，处理scrollView滚动到居中位置
			handleLeftScrollView(index) {
				const tabbarItemTop = this.tabbarItemInfo[index].top - this.scrollViewBasicTop
				if (tabbarItemTop > this.scrollViewHeight / 2) {
					this.leftScrollViewTop = tabbarItemTop - (this.scrollViewHeight / 2) + this.tabbarItemInfo[index]
						.height
				} else {
					this.leftScrollViewTop = 0
				}
			},
			// 切换对应分类的数据
			switchClassifyContent() {
				this.rightScrollViewTop = 1
				this.$nextTick(() => {
					this.rightScrollViewTop = 0
				})
				// console.log("当前分类名称是"+this.tabbar[this.currentTabbarIndex]); 
				this.currentstr = this.tabbar[this.currentTabbarIndex]
				//重新获取当前月份的数据
				this.getBingData(this.tabbar[this.currentTabbarIndex]);

			}
		}
	}
</script>

<style lang="scss" scoped>
	.page-b {
		min-height: 100vh;
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



	/* 自定义导航栏内容 start */
	.custom-nav {
		height: 100%;

		&__back {
			margin: auto 30rpx;
			margin-right: 10rpx;
			flex-basis: 5%;
			width: 100rpx;
			position: absolute;
		}
	}


	.left-width {
		flex-basis: 28%;
	}

	/* 自定义导航栏内容 end */
	.tn-classify {

		/* 搜索栏 start */
		&__search {
			&--wrap {}

			&__box {
				flex: 1;
				text-align: center;
				padding: 20rpx 30rpx;
				margin: 0 30rpx;
				border-radius: 60rpx;
				font-size: 30rpx;
			}

			&__icon {
				padding-right: 10rpx;
			}

			&__text {
				padding-right: 10rpx;
			}
		}

		/* 搜索栏 end */

		/* 分类列表和内容 strat */
		&__container {}

		&__left-box {}

		&__right-box {
			background-color: #FFFFFF;
		}

		/* 分类列表和内容 end */

		/* 侧边导航 start */
		&__tabbar {
			&__item {
				height: 110rpx;

				&:first-child {
					border-top-right-radius: 0rpx;
				}

				&:last-child {
					border-bottom-right-radius: 0rpx;
				}

				&--active {
					background-color: #FFFFFF;
					position: relative;
					// font-weight: bold;
					color: #3668FC;

					&::before {
						content: '';
						position: absolute;
						width: 12rpx;
						height: 40rpx;
						top: 50%;
						left: 0;
						background-color: #3668FC;
						border-radius: 12rpx;
						transform: translateY(-50%) translateX(-50%);
					}

					&--prev {
						border-bottom-right-radius: 26rpx;
					}

					&--next {
						border-top-right-radius: 26rpx;
					}
				}
			}
		}

		/* 侧边导航 end */

		/* 分类内容 start */
		&__content {
			margin: 18rpx;

			/* 推荐商品 start */
			&__recomm {
				margin-bottom: 40rpx;

				&__swiper {}
			}

			/* 推荐商品 end */

			/* 子栏目 start */
			&__sub-classify {
				margin-bottom: 20rpx;
				padding-bottom: 40rpx;

				&--title {
					font-weight: bold;
					margin-bottom: 18rpx;
				}

				&__content {

					&__item {
						width: 50%;
					}

					&__image {
						background-color: rgba(188, 188, 188, 0.1);
						border-radius: 10rpx;
						margin: 10rpx 10rpx 0 10rpx;
						margin-left: 0;
						width: 100%;
						height: 340rpx;
						overflow: hidden;
						border: 1rpx solid rgba(0, 0, 0, 0.02);

						image {
							width: 100%;
							height: 340rpx;
						}
					}

					&__title {
						margin-right: 10rpx;
					}
				}
			}

			/* 子栏目 end */
		}

		/* 分类内容 end */
	}
</style>
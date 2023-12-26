<template>

	<view class="components-time-line">

		<!-- 顶部自定义导航 -->
		<tn-nav-bar fixed>时光机</tn-nav-bar>

		<!-- 页面内容 -->
		<view :style="{paddingTop: vuex_custom_bar_height + 'px'}">
			<view class="time-line__wrap">
				<tn-time-line>
					<block v-for="(item, index) in componentsTimeline" :key="index">
						<tn-time-line-item  :top="2">
							<template>
							  <view class="time-line-item__node">
								<view  class="time-line-item__node--icon tn-icon-zodiac-zhu"></view>
							   </view>
							</template>
							<template slot="content">
								<view>
									<view class="time-line-item__content__title">{{ item.test }}</view> 
									<view class="time-line-item__content__desc"> <view v-if="item.agent=='weixin'">
										 来自微信
									 </view>
									 <view v-else>
									    来自浏览器
									 </view></view>
									<view class="time-line-item__content__time">
										{{ item.createdTime }} 
									</view>
								</view>
							</template>
						</tn-time-line-item>
					</block>
				</tn-time-line>
		</view>
		</view>

    <!-- 回到首页悬浮按钮-->
    <nav-index-button></nav-index-button>
	</view>
		
</template>

<script>
	// import { getTimemachineList } from '@/api/blog.js';
	import NavIndexButton from '@/libs/components/nav-index-button.vue'
	export default {
		name: 'componentsTimeline',
		data() {
			return {
				status: 'loadmore',
				// 分页请求参数
				param: {
					page: 1,
					pageSize: 15
				},
				//时光机列表
				componentsTimeline: []
			}
		},
		
		components: {
			NavIndexButton
		},methods: {
			/**
			 * 获取时光机分页列表
			 * */
			getcomponentsTimeline() {
				this.status = 'loading'
				// getTimemachineList(this.param).then(res => {
				// 	if (res.data.success && res.data.code == 200) {
				// 		this.componentsTimeline.push(...res.data.data.rows);
				// 		this.status = 'loadmore'
				// 	} else {
				// 		console.log("获取数据失败了")
				// 	}
				// }).catch(resolve => {
				// 	console.log("获取数据异常了", resolve)
				// })
			},
			// 瀑布流加载完毕事件
			// handleWaterFallFinish() { 
			// 	this.param.page++;
			// 	console.log("瀑布流加载完毕事件");
			// 	/* 瀑布流*/
			// 	this.getcomponentsTimeline() 
			// 	this.loadStatus = 'loadmore'
			// }

		},
		created() {
			this.getcomponentsTimeline();
		},
		onReachBottom() {
			this.param.page++;
			this.getcomponentsTimeline();

		}
	}
</script>

<style lang="scss" scoped>
	// 小圆点的实现
	.status-point{
		 // display: inline-block;
		  width: 6px;
		  height: 6px;
		  border-radius: 50%;
	}
	
	.tn-time-line-class {
		.tn-time-line-item-class {
			&:first-child {
				.tn-time-line-item__node {
					.time-line-item__node {
						background-color: $tn-main-color !important;
					}
				}
			}
		}
	}

	.time-line {

		&__wrap {
			padding: 60rpx 30rpx 30rpx 60rpx;
		}

		&-item {
			&__node {
				width: 44rpx;
				height: 44rpx;
				border-radius: 100rpx;
				display: flex;
				align-items: center;
				justify-content: center;
				background-color: #000000;

				&--active {
					background-color: $tn-main-color;
				}

				&--icon {
					color: #FFFFFF;
					font-size: 24rpx;
				}
			}

			&__content {
				&__title {
					font-weight: bold;
					font-size: 32rpx;
				}

				&__desc {
					color: $tn-font-sub-color;
					font-size: 28rpx;
					margin-bottom: 6rpx;
				}

				&__time {
					color: $tn-font-holder-color;
					font-size: 26rpx;
				}
			}
		}
	}
</style>

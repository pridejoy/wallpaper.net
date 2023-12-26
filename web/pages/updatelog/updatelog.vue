<template>

	<view class="components-time-line">

		<!-- 顶部自定义导航 -->
		<tn-nav-bar fixed>更新日志</tn-nav-bar>

		<!-- 页面内容 -->
		<view :style="{paddingTop: vuex_custom_bar_height + 'px'}">

			<view class="time-line__wrap">
				<tn-time-line>
					<block v-for="(item, index) in expressData" :key="index">
						<tn-time-line-item v-if="item.status !== 0" :top="2">
							<template slot="node">
								<view class="time-line-item__node">
									<view v-if="item.status === 2019" class="time-line-item__node--icon tn-icon-zodiac-zhu"></view>
									<view v-if="item.status === 2022" class="time-line-item__node--icon tn-icon-zodiac-hu"> </view>
									<view v-if="item.status === 2023" class="time-line-item__node--icon tn-icon-zodiac-tu"> </view>
									<view v-if="item.status === 2024" class="time-line-item__node--icon tn-icon-zodiac-long"> </view>
									<view v-if="item.status === 2025" class="time-line-item__node--icon tn-icon-zodiac-she"></view>
									<view v-if="item.status === 2026" class="time-line-item__node--icon tn-icon-zodiac-ma"></view>
									<view v-if="item.status === 2027" class="time-line-item__node--icon tn-icon-zodiac-yang"> </view>
								</view>
							</template>
							<template slot="content">
								<view>
									<view v-if="item.status === 2019" class="time-line-item__content__title">2019</view>
									<view v-if="item.status === 2022" class="time-line-item__content__title">2022</view>
									<view v-if="item.status === 2023" class="time-line-item__content__title">2023</view>
									<view v-if="item.status === 2024" class="time-line-item__content__title">2024</view>
									<view v-if="item.status === 2025" class="time-line-item__content__title">2025</view>
									<view v-if="item.status === 2026" class="time-line-item__content__title">2026</view>
									<view v-if="item.status === 2027" class="time-line-item__content__title">2027</view>
									<view class="time-line-item__content__desc">{{ item.info }}</view>
									<view class="time-line-item__content__time">{{ item.time }}</view>
								</view>
							</template>
						</tn-time-line-item>
						<tn-time-line-item v-else>
							<template slot="content">
								<view>
									<view class="time-line-item__content__desc">{{ item.info }}</view>
									<view class="time-line-item__content__time">{{ item.time }}</view>
								</view>
							</template>
						</tn-time-line-item>
					</block>
				</tn-time-line>
			</view>

		</view>

	</view>

</template>

<script>
	export default {
		name: 'componentsTimeline',
		data() {
			return {
				expressData: [
					{
						info: 'V1版本发布:页面采用原生开发，后台接口为typecho提供，博客为handsome',
						status: 2019,
						time: '2019-03-10 12:17',
					},	{
						info: '修复一言接口',
						status: 0,
						time: '2020-03-10 10:25'
					},{
						info: 'V2版本更新，重构Ui,采用Uniapp,TuniaoUi，采用Net6重构接口',
						status: 2022,
						time: '2022-04-03 18:54'
					},{
						info: '登录修复,添加抖音去水印功能，新增页面分享功能',
						status: 0,
						time: '2022-09-23 17:50'
					},
					{
						info: '删除抖音去水印，新增每日必应壁纸模块',
						status: 2023,
						time: '2023-03-02 17:50'
					},
					{
						info: '新增历史必应模块，瀑布流壁纸',
						status: 0,
						time: '2023-03-09 17:50'
					},
					{
						info: '新增 优化 Random Gril模块，重构我的',
						status: 0,
						time: '2023-03-09 17:50'
					},
												
				]
			}
		},
		methods: {

		}

	}
</script>

<style lang="scss" scoped>
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
				background-color: #AAAAAA;

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

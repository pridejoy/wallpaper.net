<template>
	<view>
		<!-- 顶部自定义导航 -->
		<tn-nav-bar>观影记录</tn-nav-bar>
		<!-- 页面内容 -->
		<view :style="{paddingTop: vuex_custom_bar_height + 'px'}">
			<view class="stickytdiv">
				<tn-sticky class="stickytop" :customNavHeight="vuex_custom_bar_height" offsetTop="16">
					<view class="stickymain">
						<tn-avatar size="100rpx" src="http://imagesoss.hunji.xyz/logo.png"></tn-avatar>
						<view class="stickyfont">在豆瓣一共标记看过218部影视</view>
					</view>
				</tn-sticky>
			</view>

			<!-- 图文 -->

			<!-- 比例 start-->
			<view class="tn-flex tn-flex-wrap tn-margin-sm">
				<block v-for="(item, index) in content" :key="index">
					<view class="" style="width: 50%;">
						<view class="tn-blogger-content__wrap1">
							<view class="image-pic" :style="'background-image:url(' + item.douBanCover + ')'">
								<view class="image-music">
								</view>
							</view>
							<view class="tn-blogger-content__label tn-text-justify tn-padding-sm  shenluo">
								<text class="tn-blogger-content__label__desc  ">{{ item.douBanName }}</text>
							</view>
							<view
								class="tn-flex tn-flex-row-between tn-flex-col-center tn-padding-left-sm tn-padding-right-sm tn-padding-bottom-sm">
								<view class="justify-content-item tn-flex tn-flex-col-center">
									<view>
										<view class="tn-color-gray">
											<text class="tn-blogger-content__count-icon tn-icon-flower"></text>
											<text class="tn-padding-right-sm">{{ item.watchDate|Fdate }}</text>
										</view>
									</view>
								</view>
							</view>
						</view>
					</view>
				</block>
			</view>
			<!-- 比例 end-->
			<!-- 回到首页悬浮按钮-->
			<nav-index-button></nav-index-button>
		</view>
	</view>
</template>

<script>
	// import {
	// 	getDoubanMoviePage
	// } from '@/api/blog.js'
	import NavIndexButton from '@/libs/components/nav-index-button.vue'
	export default {
		data() {
			return { // 分页请求参数
				param: {
					page: 1,
					pageSize: 15
				},
				content: []
			}
		},
		components: {
			NavIndexButton
		},
		methods: {
			getmoviedata() {
				// getDoubanMoviePage(this.param).then(res => {
				// 	if (res.data.success && res.data.code == 200) {
				// 		console.log("获取数据", res.data.data.rows)
				// 		this.content.push(...res.data.data.rows);
				// 	} else {
				// 		uni.showToast({
				// 			icon: 'error',
				// 			title: '获取数据失败了' + resolve
				// 		});
				// 	}
				// }).catch(resolve => {
				// 	uni.showToast({
				// 		icon: 'error',
				// 		title: '获取数据异常了' + resolve
				// 	});
				// })
			}
		},
		created() {
			this.getmoviedata();
		},
		onReachBottom() {
			this.param.page++;
			this.getmoviedata();

		}
	}
</script>

<style scoped>
	.stickytdiv {
		padding-top: 16rpx;
		background-color: #fafafa;
		text-align: center;

	}



	.stickyfont {
		color: black;
	}

	.stickymain {
		width: 90%;
		margin-top: 10rpx;
		/* border: 1px solid red; */
		margin: 0px auto;
		background-color: #fafafa;
		border-radius: 20rpx;
	}



	.shenluo {
		width: 160px;
		overflow: hidden; //超出隐藏
		white-space: nowrap; //不折行
		text-overflow: ellipsis; //溢出显示省略号
	}



	.tn-blogger-content__wrap1 {
		box-shadow: 0rpx 0rpx 50rpx 0rpx rgba(0, 0, 0, 0.12);
		border-radius: 20rpx;
		margin: 20rpx;
	}

	.image-pic {
		background-size: cover;
		background-repeat: no-repeat;
		/* background-attachment:fixed; */
		background-position: top;
		border-radius: 20rpx 20rpx 0 0;
	}

	.image-music {
		padding: 200rpx 0rpx;
		font-size: 16rpx;
		font-weight: 300;
		position: relative;
	}
</style>

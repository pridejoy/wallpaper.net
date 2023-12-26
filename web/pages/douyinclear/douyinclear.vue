<template>
	<view class="template-photo">
		<!-- 顶部自定义导航 -->
		<!-- <tn-nav-bar fixed>去水印</tn-nav-bar> -->
		<!-- 页面内容 -->
<!-- 		<view :style="{paddingTop: vuex_custom_bar_height+5 + 'px'}">
			<view class="platforms">
				<video id="myVideo" style="width: 100%;" v-if="!buttonstatus" :src="douyinurl"
					@error="videoErrorCallback" controls></video>
			</view>
		</view> -->
<!-- 		<view class="douyinmain">
			<form :model="form" @submit="formSubmit" @reset="formReset">
				<input type="text" v-model="form.douyinstr" style="height: 60px;  border: black 1px dashed "
					placeholder="复制抖音分享的链接粘贴到这里">
				<view style="margin-top: 20px;">
					<button form-type="submit" v-if="buttonstatus">解析</button>
					<button type="default" @click="downloadVoid()" v-else>保存</button>
				</view>
			</form>
		</view> -->

	</view>
</template>

<script>
	// import {
	// 	claerdouyin
	// } from '@/utils/api/tools.js';;
	import template_page_mixin from '@/libs/mixin/template_page_mixin.js'
	export default {
		mixins: [template_page_mixin],
		data() {
			return {
				form: {
					douyinstr: "",
				},
				buttonstatus: true, //true是解析状态, 反之false为保存 ，
				douyinurl: "" //接口返回的抖音在线播放接口
			}
		},
		onReady: function(res) {
			this.videoContext = uni.createVideoContext('myVideo')
		},
		careat(){
		uni.showLoading({
			title: '解析中'
		});
		},
		methods: {
			/**
			 * 提交获取视频链接
			 * */
			formSubmit() {
				uni.showLoading({
					title: '解析中'
				});
				// claerdouyin(this.form).then(res => {
				// 	if (res.data.success && res.data.code == 200) {
				// 		console.log("获取数据", res.data.data)
				// 		this.douyinurl = res.data.data;
				// 		this.buttonstatus = !this.buttonstatus;
				// 	} else {
				// 		uni.showToast({
				// 			icon: 'error',
				// 			title: res.data.message
				// 		});
				// 	}
				// }).catch(resolve => {
				// 	uni.showToast({
				// 		icon: 'error',
				// 		title: '获取数据异常了' + resolve
				// 	});
				// })
			   
			},
			/**
			 *保存视频到本地
			 * **/
			downloadVoid() {
				uni.showLoading({
					title: '保存中'
				});
				let that = this ;
				uni.downloadFile({
					url: this.douyinurl,
					success: (res) => {
						if (res.statusCode === 200) {
							uni.saveVideoToPhotosAlbum({ //保存图片到系统相册。
								filePath: res.tempFilePath, //图片文件路径
								success: function() {
									uni.showToast({
										title: '保存成功',
										icon: 'none',
									});

									//清除以前保存的信息
									that.form.douyinstr = "";
									that.buttonstatus = true;
								},
								fail: function(e) { 
									uni.showToast({
										title: '保存失败',
										icon: 'error',
									});
									//变成解析的状态
									that.buttonstatus = true;
								}
							})
						}
						else{
							uni.showToast({
								title: '获取视频链接失败',
								icon: 'error',
							});
							//变成解析的状态
							that.buttonstatus = true;
						}
					}
				}); 
			
			}
		}
	}
</script>

<style lang="scss" scoped>
	.platforms {
		background-color: #99CCFF;
		height: 400rpx;
		width: 100%;
		// border: red 1px solid;
	}

	.douyinmain {
		margin-top: 20px;
	}
</style>

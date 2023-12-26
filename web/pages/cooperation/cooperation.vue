<template>
	<view>
		<!-- 顶部自定义导航 -->
		<tn-nav-bar fixed>想说的话</tn-nav-bar>
			<view :style="{paddingTop: vuex_custom_bar_height + 'px'}">		
			<view class="cooperbody">
				<image class="cooperimg"  src="http://imagesoss.hunji.xyz/tuniao/my-qrcode.png" mode="heightFix"  @click="prevMainImage()"></image> 
				<view class="cooperih2"> 联系方式：</view>
				<view>
					<ui>
						<li>QQ: 514324468</li>
						<li>Wx: Handsome_mar(点击图片可打开)</li>
						<li>Email: pridejoy@foxmail.com</li>
						<li>Gitee: https://gitee.com/Pridejoy</li>
						<li>Github:https://github.com/PrideJoy</li>
					</ui>
				</view>
			</view>
		 <!-- <image src='http://imagesoss.hunji.xyz/tuniao/my-qrcode.png' style="width: 500rpx;" mode='widthFix'></image> -->
		 </view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				
			}
		},
		methods: {
			/*
			*打开主图进行预览
			* */
			prevMainImage() {				 
				uni.previewImage({
					urls: ["http://imagesoss.hunji.xyz/tuniao/my-qrcode.png"],
					current: 0
				});
			}
		}
	}
</script>

<style scoped>
.cooperbody{
	width: 90%;
	margin: 30rpx auto; 
}

.cooperimg{ 
	height: 400rpx;  
	display: flex;
	justify-content: center;
}
.cooperih2{
	margin-top: 20rpx;
	font-size: 50rpx;
	font-weight: 200;
	text-align:left;
}

</style>

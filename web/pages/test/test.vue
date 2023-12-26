<template>  
	<view :style="{paddingTop: vuex_custom_bar_height + 'px'} " style="border: 1px red solid;" >


     <view>预览图片</view>
		<view class="photosView">
			<block v-for="(currentImg, index) in imgs" :key="index">
				<view class="box">
					<img :src="currentImg.src" :style="{ width: currentImg.width + 'px', height: currentImg.height + 'px' }">
					<!-- <image :src="item.src" mode="widthFix" @click="previewImage(index)"></image> -->
					</view>
			</block>
		</view>
	</view>
	
</template>

<script>
	export default {
		data() {
			return {
				 currentIndex: 0,
				      imgs: [
				        { src: 'http://imagesoss.hunji.xyz/randomgril/Other/980_2017-04-11_20-33-06.png', width: 600, height: 400 },
				        { src: 'http://imagesoss.hunji.xyz/randomgril/Other/981_2017-04-10_18-49-34.png', width: 600, height: 900 }, 
				      ]
			}
		},
		computed: {
		    currentImg() {
		      return this.imgs[this.currentIndex]
		    }
		  },
		methods: {
			 preview(index) {
			      this.currentIndex = index
			      // 显示预览图片
			      document.querySelector('.preview').style.display = 'flex'
			    },
			    closePreview() {
			      // 隐藏预览图片
			      document.querySelector('.preview').style.display = 'none'
			    }
		}
	}
</script>

<style>
.preview {
  background-color: rgba(0, 0, 0, 0.8);
  position: fixed;
  top: 1000;
  left: 0;
  width: 100%;
  height: 100%;
  display: none;
  justify-content: center;
  align-items: center;
}
.preview img {
  max-width: 100%;
  max-height: 100%;
  object-fit: contain;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.8);
}
</style>

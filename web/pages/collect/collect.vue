<template>
  <view class="tn-safe-area-inset-bottom">
<!-- 顶部自定义导航 -->
    <tn-nav-bar fixed customBack :bottomShadow="false" :backgroundColor="navBarBackgroundColor" id="navbar">
      <view slot="back" class='tn-custom-nav-bar__back' @click="goBack" :style="[navBarStyle]">
        <text class='icon tn-icon-left'></text>
        <text class='icon tn-icon-discover-fill'></text>
      </view>
      <view class="tn-flex tn-flex-col-center tn-flex-row-center" :style="[navBarStyle2]">
        <view class="tn-text-bold tn-text-xl"></view>
      </view>
    </tn-nav-bar>
    
    <swiper class="card-swiper" :circular="true"
      :autoplay="true" duration="500" interval="22000" @change="cardSwiper"> 
      <swiper-item v-for="(item,index) in swiperList" :key="index" :class="cardCur==index?'cur':''">
        <view class="swiper-item image-banner">
          <image :src="item.url" mode="aspectFill" v-if="item.type=='image'"></image>
        </view>
      </swiper-item>
    </swiper>
    <view class="indication">
        <block v-for="(item,index) in swiperList" :key="index">
            <view class="spot" :class="cardCur==index?'active':''"></view>
        </block>
    </view>
    
    <!-- 头部start-->
    <view class="shadow-function tn-margin-bottom-xl" :style="{paddingTop: vuex_custom_bar_height + 'px'}">
      <view class="tn-flex tn-flex-direction-column tn-flex-row-center tn-flex-col-center">
        <view class="justify-content-item tn-flex-row-center">
          <view class="logo-pic tn-shadow">
            <view class="logo-image">
              <view class="tn-shadow-blur" style="background-image:url('http://imagesoss.hunji.xyz/conch-net/avatar/1.png');width: 110rpx;height: 110rpx;background-size: cover;">
              </view>
            </view>
          </view>
        </view>
        <view class="justify-content-item">
          <view class="tn-flex tn-flex-col-center">
            <view class="tn-color-white">
              <view class="tn-text-center tn-text-lg tn-text-bold tn-margin-sm">
                
              </view>
              <view class="tn-text-ellipsis tn-text-sm">
                <text class="">请签收来自秋天的浪漫</text>
              </view>
            </view>
          </view>
        </view>

      </view>
    </view>
    <!-- 头部 end-->
    
    <tn-modal v-model="show1" :custom="true">
      <view class="custom-modal-content">
        <image @tap="previewQRCodeImage" src='http://imagesoss.hunji.xyz/qrcode_20230428092627.jpg' mode='aspectFill' style="width: 100%;"></image>
        <!-- <view class="tn-text-center tn-padding-top">欢迎加入【图鸟交流】群</view> -->
        <view class="tn-text-center tn-padding-top tn-text-lg">点击上图，可识别微信二维码</view>
      </view>
    </tn-modal>
    
    <view class="group-wrap tn-bg-white" id="page_tips">
      
      <!-- 悬浮按钮-->
      <view class="tn-flex tn-flex-row-between" style="padding: 60rpx 0 30rpx 0;">
        <view class="tn-flex-1 justify-content-item tn-margin-xs tn-text-center">
          <tn-button backgroundColor="#3668fc" padding="40rpx 0" width="90%" fontBold @click="showModal">
            <text class="tn-icon-wechat tn-padding-right-xs tn-color-white"></text>
            <text class="tn-color-white">微 信</text>
          </tn-button>
        </view>
        <view class="tn-flex-1 justify-content-item tn-margin-xs tn-text-center">
          <tn-button backgroundColor="#fbbd12" padding="40rpx 0" width="90%" fontBold open-type="share">
            <text class="tn-icon-send tn-padding-right-xs tn-color-white"></text>
            <text class="tn-color-white">分 享</text>
          </tn-button>
        </view>
      </view>
      
      <view class="" style="padding: 30rpx 20rpx;" >
          <tn-waterfall ref="waterfall" v-model="list" @finish="handleWaterFallFinish">
            <template v-slot:left="{ leftList }">
              <view v-for="(item, index) in leftList" :key="item.id" class="product__item" @click="tn('/pages/randomgril/randomgril?type='+item.type+'&shareimgid='+item.imagesID)">
                <view class="item__image">
                  <tn-lazy-load :threshold="6000" height="100%" :image="item.imagesURL" :index="item.imagesID" imgMode="widthFix"></tn-lazy-load>
                </view>
                <view class="item__data">
                  <view class="item__title-container">
                    <!-- <view v-if="item.newProduct" class="item__store-type tn-bg-gray--light">标签</view>
                    <view v-else-if="item.storeType === 1" class="item__store-type tn-cool-bg-color-1">SVIP</view> -->
                    <text class="item__title tn-color-black">{{ item.type }}</text>
                  </view>
           <!--       <view v-if="item.tags && item.tags.length > 0" class="item__tags-container">
                    <view v-for="(tagItem, tagIndex) in item.tags" :key="tagIndex" class="item__tag">{{ tagItem }}</view>
                  </view> -->
              
                </view>
              </view>
            </template>
            <template v-slot:right="{ rightList }">
            	  <view v-for="(item, index) in rightList" :key="item.id" class="product__item" @click="tn('/pages/randomgril/randomgril?type='+item.type+'&shareimgid='+item.imagesID)">	  
                <view class="item__image">
                  <tn-lazy-load :threshold="6000" height="100%" :image="item.imagesURL" :index="item.imagesID" imgMode="widthFix"></tn-lazy-load>
                </view>
                <view class="item__data">
                  <view class="item__title-container">
                    <!-- <view v-if="item.storeType === 1" class="item__store-type tn-cool-bg-color-5">VIP</view> -->
                    <text class="item__title tn-color-balck">{{ item.type }}</text>
                  </view>
             <!--     <view v-if="item.tags && item.tags.length > 0" class="item__tags-container">
                    <view v-for="(tagItem, tagIndex) in item.tags" :key="tagIndex" class="item__tag">{{ tagItem }}</view>
                  </view> -->
                  
                </view>
              </view>
            </template>
          </tn-waterfall>
        </view>
        <tn-load-more :status="loadStatus"></tn-load-more>
      
    </view>
  </view>
</template>

<script>
  import template_page_mixin from '@/libs/mixin/template_page_mixin.js'
  import {likeImgList} from '@/utils/api/gallery.js'
  export default {
    name: 'TemplateAuthor',
    mixins: [template_page_mixin],
    data(){
      return {
        show1: false,
        cardCur: 0,
        swiperList: [{
          id: 0,
          type: 'image',
          url: 'https://cdn.nlark.com/yuque/0/2022/jpeg/280373/1664015047023-assets/web-upload/147b0b7f-8253-4b92-bc1d-e28db7f54096.jpeg',
        } ], 
        
        /* 导航*/
        navBarRectInfo: {},
        navBarChangebaseLineHeight: 0,
        navBarStyle: {
          color:'#FFFFFF',
          opacity: 1,
          display: 'flex'
        },
        navBarStyle2: {
          color:'rgba(255,255,255,0)',
          opacity: 1,
          display: 'flex'
        },
        navBarBackgroundColor: 'rgba(255, 255, 255, 0)',
        activeBgAnimation: {},
        
        /* 瀑布流*/
        loadStatus: 'loadmore',
		//瀑布流数据
        list: [], 
		param:{
			Page:1,
			PageSize:20
		}
      }
    },

    onLoad() {
      this.getRandomData()
    },
    onReachBottom() {
      this.getRandomData()
    },
    
    onReady() {
      this.$nextTick(() => {
        this.initNavBarRectInfo()
      })
    },
    onPageScroll() {
      this.updateNavBarRectInfo()
    },
    
    methods: {
      // 跳转
      tn(e) {
      	uni.navigateTo({
      		url: e,
      	});
      },
      
      // cardSwiper
      cardSwiper(e) {
        this.cardCur = e.detail.current
      },
      // 预览圈子管理员微信图片
      previewQRCodeImage() {
        wx.previewImage({
        urls: ['http://imagesoss.hunji.xyz/qrcode_20230428092627.jpg']
        })
      },
      // 弹出模态框
      showModal(event) {
        this.openModal()
      },
      // 打开模态框
      openModal() {
        this.show1 = true
      },
      // 初始化导航栏信息
      async initNavBarRectInfo() {
        const navBarRectInfo = await this._tGetRect('#navbar')
        const pageTipsRectInfo = await this._tGetRect('#page_tips')
        // console.log(navBarRectInfo, pageTipsRectInfo, navBarRectInfo?.top, pageTipsRectInfo?.top);
        if (!navBarRectInfo.hasOwnProperty('top') || !pageTipsRectInfo.hasOwnProperty('top')) {
          setTimeout(() => {
            this.initNavBarRectInfo()
          }, 10)
          return
        }
        this.navBarRectInfo = {
          top: navBarRectInfo.top
        }
        this.navBarChangebaseLineHeight = pageTipsRectInfo.top - navBarRectInfo.top
      },
      // 更新导航栏信息
      updateNavBarRectInfo() {
        this._tGetRect('#page_tips').then((res) => {
          const top = res?.top || 0
          if (!top) {
            return
          }
          const differHeight = top - this.navBarRectInfo.top
          const opacity = differHeight / this.navBarChangebaseLineHeight
          if (opacity < 0) {
            // this.navBarStyle.opacity = 1
            // this.navBarStyle.display = 'flex'
            this.navBarStyle.color = 'rgba(0, 0, 0, ${opacity})'
            this.navBarStyle2.color = 'rgba(0, 0, 0, ${opacity})'
            this.navBarBackgroundColor = `rgba(255, 255, 255, 1)`
          } else {
            // this.navBarStyle.opacity = 1 - opacity
            // this.navBarStyle.display = 'flex'
            this.navBarStyle.color = 'rgba(255, 255, 255, 1)'
            this.navBarStyle2.color = 'rgba(255, 255, 255, 0)'
            this.navBarBackgroundColor = `rgba(255, 255, 255, ${1 - opacity})`
          }
          
          // console.log(top, differHeight, opacity);
        })
      },
      /* 瀑布流*/
      // 获取随机数据
      getRandomData() { 
        this.loadStatus = 'loading' 
		likeImgList(this.param).then(res => {
			console.log(res)
			if ( res.code == 200) {
				//数据加载完
				this.param.Page++;
					console.log("获取数据失败了",res.body.rows.length )
				if (res.body.rows.length == 0) {
					this.loadStatus = 'loadmore'
				}
				this.list.push(...res.body.rows); 
			} else {
				console.log("获取数据失败了")
			}
		}).catch(resolve => {
			console.log("获取数据异常了", resolve)
		})
        // for (let i = 0; i < 10; i++) {
        //   let index = this.$t.number.randomInt(0, this.data.length - 1)
        //   let item = JSON.parse(JSON.stringify(this.data[index]))
        //   item.id = this.$t.uuid()
        //   this.list.push(item)
        // }
      },
      // 瀑布流加载完毕事件
      handleWaterFallFinish() {
        this.loadStatus = 'loadmore'
      },
    }
  }
</script>

<style lang="scss" scoped>
  /* 胶囊*/
  .tn-custom-nav-bar__back {
    width: 100%;
    height: 100%;
    position: relative;
    display: flex;
    justify-content: space-evenly;
    align-items: center;
    box-sizing: border-box;
    background-color: rgba(0, 0, 0, 0.03);
    border-radius: 1000rpx;
    border: 1rpx solid rgba(255, 255, 255, 0.5);
    // color: #FFFFFF;
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
      opacity: 0.05;
      background-color: #000000;
    }
  }
  
  /* 轮播视觉差 start */
  .card-swiper {
    height: 500rpx !important;
  }
    
  .card-swiper swiper-item {
    width: 750rpx !important;
    left: 0rpx;
    box-sizing: border-box;
    // padding: 0rpx 30rpx 90rpx 30rpx;
    overflow: initial;
  }
    
  .card-swiper swiper-item .swiper-item {
    width: 100%;
    display: block;
    height: 100%;
    transform: scale(1);
    transition: all 0.2s ease-in 0s;
    will-change: transform;
    overflow: hidden;
  }
    
  .card-swiper swiper-item.cur .swiper-item {
    transform: none;
    transition: all 0.2s ease-in 0s;
    will-change: transform;
  }
  
  .image-banner{
    display: flex;
    align-items: center;
    justify-content: center;
  }
  .image-banner image{
    width: 100%;
    height: 100%;
  }
  
  /* 轮播指示点 start*/
  .indication{
    z-index: 9999;
    width: 100%;
    height: 36rpx;
    position: absolute;
    display:flex;
    flex-direction:row;
    align-items:center;
    justify-content:center;
    opacity: 0;
  }
  
  .spot{
    background-color: #FFFFFF;
    opacity: 0.6;
    width: 10rpx;
    height: 10rpx;
    border-radius: 20rpx;
    top: -60rpx;
    margin: 0 8rpx !important;
    position: relative;
  }
  
  .spot.active{
    opacity: 1;
    width: 30rpx;
    background-color: #FFFFFF;
  }
  
  /* 顶部 */
  .shadow-function {
      position: relative;
      z-index: 1;
      margin-top: -450rpx;
      padding-bottom: 110rpx;
      background-image: linear-gradient(rgba(255, 255, 255, 0.01), rgba(0, 0, 0, 0.4));
  }
  
  /* 用户头像 start */
  .logo-image {
    width: 110rpx;
    height: 110rpx;
    position: relative;
  }
  
  .logo-pic {
    background-size: cover;
    background-repeat: no-repeat;
    // background-attachment:fixed;
    background-position: top;
    border: 6rpx solid rgba(255,255,255,0.25);
    box-shadow: 0rpx 0rpx 80rpx 0rpx rgba(0, 0, 0, 0.15);
    border-radius: 50%;
    overflow: hidden;
    // background-color: #FFFFFF;
  }
  
  /* 阴影 start*/
  .group-shadow {
    border-radius: 15rpx;
    box-shadow: 0rpx 0rpx 50rpx 0rpx rgba(0, 0, 0, 0.07);
  }
  
  
  /* 内容*/
  .group-wrap {
    position: relative;
    z-index: 1;
    // padding: 20rpx 30rpx;
    margin-top: -130rpx;  
    margin-bottom: 40rpx;
    border-radius: 30rpx 30rpx 0 0;
  }
  
  /* 阴影 start*/
  .mould-shadow {
    border-radius: 15rpx;
    box-shadow: 0rpx 0rpx 50rpx 0rpx rgba(0, 0, 0, 0.07);
  }    
  
  .tn-tabbar-height {
  	min-height: 20rpx;
  	height: calc(40rpx + env(safe-area-inset-bottom) / 2);
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
  
  
  /* 瀑布流*/
  .product__item {
    background-color: #FFFFFF;
    overflow: hidden;
    margin: 0 10rpx;
    margin-bottom: 10rpx;
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
      &__store-type {
        height: 28rpx;
        font-size: 20rpx;
        position: relative;
        display: inline-flex;
        align-items: center;
        justify-content: center;
        padding: 4rpx;
        border-radius: 6rpx;
        white-space: nowrap;
        text-align: center;
        top: -2rpx;
        margin-right: 6rpx;
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

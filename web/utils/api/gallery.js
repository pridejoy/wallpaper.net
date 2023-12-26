import request from "@/utils/request"
 
//获取分类
export function getCategorize(params) {
	return request.get("/api/GalleryService/categorize").then(res => {
		return res.data
	}) 
}
 
//获取一张随机壁纸
export function getRandom(params) {
	return request.get("/api/GalleryService/randomimg",  {
		params: params,
	}).then(res => {
		return res
	})   

} 
//获取用户喜欢壁纸的
export function likeImgList(params) {
	return request.get("/api/GalleryService/likeimglist",{params:params}).then(res => {
		return res
	}) 
}
 
//喜欢一张壁纸
export function likeImg(params) {
	return request.get("/api/GalleryService/likeimg",{params:params}).then(res => {
		return res
	}) 
}



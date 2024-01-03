import request from "@/utils/request"


// 必应壁纸-按月份获取
export function getBingPic(params) {
	return request.post('/api/bing/every', params).then(res => {
		return res
	}) 
}
 
/**
 * 每日必应接口
 */
export function getEveryDayBingPic(params) {
	return request.get('/api/tools/ererydaybing', {params:params}).then(res => {
		return res
	}) 
}


//获取瀑布流壁纸
export function getWaterfall(params) {
	return request.get('/api/waterfall/page', {params:params}).then(res => {
		return res
	}) 
}

 

//获取随机壁纸
export function getRandomGrilNumber(params) {
	return request.get("/api/tools/number/randomgril",params).then(res => {
		return res
	}) 
}

import request from "@/utils/request"

//获取今日的必应
export function currDayBing(params) {
	return request.get("api/ElementService/CurrDayBing", {
		params: params,
	}).then(res => {
		return res
	})
}

//获取今日的必应
export function everyDayBing(data) {
	return request.post("api/ElementService/EveryDayBing", data).then(res => {
		return res
	})
}


//获取指定分类的一张照片
export function getCategImg(params) {
	return request.get("api/ElementService/GetCategImg", {
		params: params,
	}).then(res => {
		return res
	})
}

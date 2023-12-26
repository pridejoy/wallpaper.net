import request from "@/utils/request"

//发布商品
export function addGoods(params) {
	return request.post("goods/add", params)
	.then(res => { return res })
}
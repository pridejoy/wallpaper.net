
import request from "@/utils/request"


//小程序的
export function getMiniApplinkList(params) {
	return request.get("/api/MiniService/sitelink", params).then(res => {
		return res.data
	})
}

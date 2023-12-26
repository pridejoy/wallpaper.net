
import request from "@/utils/request"


//获取发布文档的
export function getHelpDoc(params) {
	return request.get("/single-server/help-doc", params).then(res => {
		return res.data
	})
}

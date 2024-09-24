import request from "@/utils/request"

export function userLogin(params) {
	return request.post("/api/Wechat/login", params).then(res => {
		console.log("123",res)
		return res.body
	})
}

export function getUserInfo() {
	return request.get("/api/Wechat/getinfo").then(res => {
		return res
	})
}

export function updateUserInfo(params) {
	return request.put("user/update", params).then(res => {
		return res.body
	})
}
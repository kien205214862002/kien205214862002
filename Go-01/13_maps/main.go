package main

import "fmt"

func main() {
	// tạo map với key là string và value là string
	dict := make(map[string]string)
	fmt.Println("dict:", dict)

	// Thêm phần tử vào map
	dict["Hello"] = "Xin chào"
	dict["Goodbye"] = "Tạm biệt"
	dict["Duck"] = "Con vịt"
	dict["Cat"] = "Con mèo"

	fmt.Println("dict:", dict)

	// Truy cập vào phần tử trong map
	fmt.Println("dict['Hello']:", dict["Hello"]) // Xin chào
	fmt.Println("dict['Dog']:", dict["Dog"])     // "" - trả ra zero value của kiểu dữ liệu nếu key k tồn tại

	// Xoá một phần tử khỏi map
	delete(dict, "Goodbye")
	fmt.Println("dict:", dict)

	// Kiểm tra xem phần tử có trong map hay không
	if cat, ok := dict["Dog"]; ok {
		fmt.Println("Cat:", cat)
	} else {
		fmt.Println("Key not found")
	}

	// Duyệt các phần tử trong map bằng for range
	for key, value := range dict {
		fmt.Printf("dict[%s] = %s\n", key, value)
	}

	// Map là kiểu tham chiếu (giống slice)
	newDict := dict

	newDict["Friend"] = "Bạn bè"
	fmt.Println("dict:", dict)
	fmt.Println("newDict:", newDict)

	addDict(dict, "Color", "Màu sắc")
	fmt.Println("dict:", dict)
}

func addDict(dict map[string]string, key, value string) {
	dict[key] = value
}

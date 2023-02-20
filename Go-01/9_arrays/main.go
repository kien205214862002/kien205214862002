package main

import "fmt"

func main() {
	// Tạo ra một mảng số nguyên độ dài là 3 và các phầ tử trong mảng sẽ mang giá trị zero value của kiểu dữ liệu số nguyên => [0, 0, 0, 0]
	var arr1 [4]int
	fmt.Println("arr1:", arr1)

	arr2 := [3]int{4, 17, 9} // => [4, 17, 9]
	fmt.Println("arr2:", arr2)

	arr3 := [...]int{1, 2, 3, 4, 5}
	fmt.Println("arr3:", arr3)

	// Kích thước của mảng là 1 phần của kiểu dữ liệu, do đó [3]int và [4]int là khác nhau.
	// arr1 = arr2

	// Mảng trong go là kiểu giá trị (value type) => Khi ta gán mảng cho một biến mới, nó sẽ tạo ra một bản copy hoàn toàn mới và gán cho biến mới. Các thay đổi giữa 2 biến này không liên quan tới nhau
	colors := [...]string{"red", "green", "blue"}
	newColors := colors
	newColors[1] = "black"
	fmt.Println("colors:", colors)
	fmt.Println("newColors:", newColors)

	changeColor(colors)
	fmt.Println("Colors outside func", colors)

	// Lấy độ dài mảng: len(array)
	nums := [...]int{3, 5, 13, 4, 6, 18, 7}
	// for i := 0; i < len(nums); i++ {
	// 	fmt.Printf("index: %d, value: %d\n", i, nums[i])
	// }

	// dùng range để duyệt mảng, nó trả ra index và value, trường hợp chỉ cần dùng 1 trong 2 thì sử dụng _
	for i, v := range nums {
		fmt.Printf("index: %d, value: %d\n", i, v)
	}
}

func changeColor(colors [3]string) {
	colors[0] = "pink"
	fmt.Println("Colors inside func", colors)
}

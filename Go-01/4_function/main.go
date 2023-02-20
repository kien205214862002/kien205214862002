package main

import "fmt"

func main() {
	price, n := 100, 8
	totalPrice := calculatePrice(price, n)
	fmt.Println("Total price:", totalPrice)

	area1, perimeter1 := rectProps(13.5, 8.5)
	fmt.Printf("Reactangle - Area: %.2f, Perimeter %.2f\n", area1, perimeter1)

	area2, perimeter2 := squareProps(6.8)
	fmt.Printf("Square - Area: %.2f, Perimeter %.2f\n", area2, perimeter2)

	// blank identifier: _
	area3, _ := rectProps(11.5, 5.5)
	fmt.Printf("Reactangle - Area: %.2f\n", area3)

}

func calculatePrice(price, n int) int {
	return price * n
}

// Cho phép return về nhiều giá trị
// Viết hàm nhận vào chiều dài và chiểu rộng, trả về diện tích và chu vi của hình chữ nhật
func rectProps(length, width float64) (float64, float64) {
	area := length * width
	perimeter := (length + width) * 2

	return area, perimeter
}

// Viết hàm nhận vào cạnh, trả về diện tich và chu vi hình vuông
// named return values
func squareProps(size float64) (area, perimeter float64) {
	area = size * size
	perimeter = size * 4

	// Vì area và perimeter đã được xác định là các giá trị trả về của hàm, nên khi gặp câu lệnh return nó sẽ tự động trả về giá trị của 2 biến này
	return
}

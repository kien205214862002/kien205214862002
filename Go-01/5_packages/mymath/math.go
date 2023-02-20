package mymath

// // Một tên biến hoặc hàm trong package sẽ được export nếu nó bắt đầu bằng chữ hoa
// var pi = 3.141516 // private variable - chỉ sử dụng được ở bên trong package
// var PI = 3.141516 // export variable - có thể được import và sử dụng ở bên ngoài package

func Sum(a, b int) int {
	return a + b
}

func CircleProps(r float64) (float64, float64) {
	area := PI * r * r
	perimeter := r * 2 * PI

	return area, perimeter
}

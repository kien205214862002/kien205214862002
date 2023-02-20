package main

import "fmt"

// Bool - kiểu luận lý
// func main() {
// 	var isActive bool // zero value của bool là false
// 	fmt.Println("isActive:", isActive)

// 	isActive = true
// 	fmt.Println("isActive:", isActive)

// 	a, b, c := true, true, false
// 	// Toán tử && (AND): Nếu tất cả giá trị là true => true
// 	d := a && b // true && true => true
// 	e := a && c // true && false => false
// 	// Toán tử || (OR): Nếu có 1 giá trị là true => true
// 	f := a || b // true || true => true
// 	g := a || c // true || false => true
// 	// Toán tử ! (phủ định): Nghich đảo giá trị bool
// 	h := !a // !true => false
// 	fmt.Println("d:", d)
// 	fmt.Println("e:", e)
// 	fmt.Println("f:", f)
// 	fmt.Println("g:", g)
// 	fmt.Println("h:", h)

// 	x, y := 3, 5
// 	z := x == y // false
// 	v := x <= y // true
// 	w := x != y // true
// 	fmt.Println("z:", z)
// 	fmt.Println("v:", v)
// 	fmt.Println("w:", w)
// }

// Number - Kiểu số
// func main() {
// 	var n int = 20
// 	fmt.Printf("value of n: %d, type of n: %T\n", n, n)

// 	// Cẩn thận bị tràn số
// 	var x, y int8 = 100, 120
// 	z := x + y
// 	fmt.Println("z:", z)

// 	h, k := 10, 7
// 	i := h / k
// 	fmt.Println("i:", i)

// 	// bitwise
// 	a, b := 3, 10
// 	// 3:  0011 => 1*2^0 + 1*2^1
// 	// 10: 1010 => 0*2^0 + 1*2^1 + 0*2^2 + 1*2^3

// 	c := a & b // 0010
// 	fmt.Printf("a & b = %d\n", c)

// 	d := a | b // 1011
// 	fmt.Printf("a | b = %d\n", d)

// 	e := a << 3 // 3 (hệ số 10) << 3 <=> 11 (hệ số 2) << 3 - Kết quả: 11000 (hệ 2) <=> 24 (hệ 10)
// 	fmt.Printf("a << 3 = %d\n", e)

// 	f := b >> 2 // 10 >> 2 <=> 1010 >> 2 - Kết quả 10 (hệ 2) <=> 2 (hệ 10)
// 	fmt.Printf("b >> 2 = %d\n", f)
// }

// Số thực - float
// func main() {
// 	x, y := 13.1519, 18.31
// 	fmt.Printf("value of x: %.2f , type of x: %T\n", x, x)

// 	sum := x + y
// 	fmt.Printf("x + y = %f\n", sum)

// 	diff := x - y
// 	fmt.Printf("x - y = %f\n", diff)

// 	z := x / y
// 	fmt.Printf("x / y = %f\n", z)

// }

// chuỗi - string
// func main() {
// 	firstName, lastName := "Dan", "Nguyen"

// 	// fullName := firstName + " " + lastName
// 	// fmt.Println(fullName)
// 	// fmt.Printf("My name: %s\n", fullName)

// 	fullName := fmt.Sprintf("My full name: %s %s", firstName, lastName)
// 	fmt.Println(fullName)
// }

// Type Assertion
func main() {
	a := 15
	b := 8.3

	// sum := a + b

	// Để ép kiểu trong go ta làm như sau: T(v)

	sum1 := a + int(b) // chuyển float 64 thành int
	fmt.Printf("a + b = %d\n", sum1)

	sum2 := float64(a) + b // chuyển int thành float64
	fmt.Printf("a + b = %.2f\n", sum2)
}

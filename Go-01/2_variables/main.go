package main

import "fmt"

// Biến toàn cục chỉ có thể được khai báo bởi cú pháp decleration (var)
var title = "Cybersoft"

// Biến toàn cục không thể khai báo bằng short-hand decleration
// title := "Cybersoft"

// Enum
const (
	Black int = (iota + 1) * 2
	White
	Red
	Green
	Blue
)

func main() {
	// Khai báo biến có tên là age và kiểu dữ liệu là int
	// mặc định biến age giá trị zero value là 0 tương ứng với kiểu dữ liệu int
	var age int
	fmt.Println("Age:", age)
	age = 20
	fmt.Println("Age:", age)
	// age = "20"

	// zero value của string là "" (chuỗi rỗng)
	// var msg string

	// Khởi tạo biến và gán giá trị mặc định
	var msg string = "Hello Go01"
	fmt.Println("Msg:", msg)

	// Type inference (suy luận kiểu)
	var username = "Alice"
	fmt.Println("Username:", username)

	// Khai báo nhiều biến trong cùng 1 câu lệnh
	// var x, y int
	var x, y = 5, 10
	fmt.Println("x:", x, "y:", y)

	// Khai báo nhiều biến trong cùng 1 câu lệnh (các biến khác kiểu dữ liệu)
	var (
		firstName string
		lastName  string
		salary    = 1000
	)
	firstName = "Dan"
	lastName = "Nguyen"
	fmt.Println(firstName, lastName, salary)

	// short-hand decleration (khai báo nhanh)
	count := 0
	fmt.Println("Count:", count)

	course, score := "GO", 8
	fmt.Println("course:", course, "score:", score)

	// Hằng số (const)
	const country = "Vietnam"
	fmt.Println("Country:", country)
	// Không thể gán lại giá trị mới cho một biến const
	// country = "US"
}

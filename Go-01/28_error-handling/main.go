package main

import (
	"fmt"
	"runtime/debug"
)

//  =========== Go style error handling ===========
// func main() {
// 	f, err := os.Open("./demo.txt")
// 	if err != nil {
// 		// customize error message
// 		fileErr := errors.New("Không tìm thấy tập tin hoặc thư mục")
// 		fmt.Println(fileErr)
// 		return
// 	}

// 	fmt.Println(f.Name())
// }

// =========== Panic/Recover ===========
func main() {
	// s := []int{1, 2, 3}
	// fmt.Println(s[4])

	username := "dannd"
	email := "kien@gmail.com"
	validateUser(&username, &email)
	fmt.Println()
	fmt.Println("Main")
}

func validateUser(username, email *string) {
	// Defer đảm bảo các câu lênh này sẽ luôn luôn được chạy khi chương trình panic
	// defer fmt.Println("Validated")
	defer recoverUser()

	if username == nil {
		panic("runtime error: username cannot be nil")
	}

	if email == nil {
		panic("runtime error: email cannot be nil")
	}

	fmt.Printf("%s - %s", *username, *email)
}

func recoverUser() {
	if r := recover(); r != nil {
		fmt.Println("Recover", r)
		// get stack trace from recover
		debug.PrintStack()
	}
}

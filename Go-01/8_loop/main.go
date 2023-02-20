package main

import "fmt"

func main() {
	for i := 1; i <= 10; i++ {
		// logic vòng lặp
		fmt.Printf(" %d", i)
	}
	fmt.Println()

	for i := 1; i <= 10; i++ {
		if i > 5 {
			break // thoát vòng lặp ngay lập tức
		}
		fmt.Printf(" %d", i)
	}
	fmt.Println()

	for i := 1; i <= 10; i++ {
		if i%2 == 0 {
			continue // bỏ qua các lệnh ở vòng lặp hiện tại mà ngay lập tức chuyển sang vòng lặp tiếp theo
		}
		fmt.Printf(" %d", i)
	}
	fmt.Println()

	// Vòng lặp vô tận
	n := 1
	for {
		fmt.Println("Go01")
		n++
		// Dùng break để thoát khỏi vòng lặp vô tận
		if n > 10 {
			break
		}
	}
}

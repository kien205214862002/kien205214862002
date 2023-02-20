package main

import (
	"fmt"
	"time"
)

func numbers() {
	for i := 1; i <= 5; i++ {
		time.Sleep(200 * time.Millisecond)
		fmt.Printf("%d ", i)
	}
}

func alphabets() {
	for i := 'a'; i <= 'e'; i++ {
		time.Sleep(400 * time.Millisecond)
		fmt.Printf("%c ", i)
	}
}

func main() {
	// Khai báo 1 gorountine ta dùng từ khoá go
	// go fmt.Println("Goroutine")
	// fmt.Println("Main")
	for i := 1; i <= 10; i++ {
		go func(n int) {
			fmt.Println(n) // Có thể tại thời điểm chạy goroutine này, vòng for đã chạy xong
		}(i)
	}
	go numbers()
	go alphabets()
	fmt.Println()

	// Khai báo goroutine cho anonymous function
	//go func() {
	//	fmt.Println("Hello Gopher")
	//}()
	fmt.Println("Hello Gopher")
	// capture variable

	time.Sleep(3 * time.Second)
}

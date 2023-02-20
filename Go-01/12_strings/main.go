package main

import (
	"fmt"
	"strings"
)

func main() {
	s := "Hello Gopher"
	// Có thể hiểu string tương đương với 1 array như sau
	// arr := [...]byte{'H', 'e', 'l', 'l', 'o', ' ', 'G', 'o', 'p', 'h', 'e', 'r'}
	fmt.Printf("s: %s\n", s)

	// Print byte
	for i := 0; i < len(s); i++ {
		fmt.Printf("%x ", s[i])
	}
	fmt.Println()

	// Print char
	for i := 0; i < len(s); i++ {
		fmt.Printf("%c ", s[i])
	}
	fmt.Println()

	// Cắt chuỗi
	s1 := s[6:]
	fmt.Printf("s1: %s\n", s1)

	// Lấy chiều dài của chuỗi bằng hàm len
	length := len(s)
	fmt.Println("length s:", length)

	name := "Nguyễn Văn Tèo"
	// printChars([]rune(name)) // Cách 1
	printChars(name) // Cách 2

	// package strings
	// contains(a, b): kiểm tra chuỗi b có nằm trong chuối a hay k
	fmt.Println("Contains:", strings.Contains("Cybersoft", "soft")) // true

}

// Cách 1: Chuyển string thành rune
// func printChars(s []rune) {
// 	for i := 0; i < len(s); i++ {
// 		fmt.Printf("%c ", s[i])
// 	}
// 	fmt.Println()
// }

// Cách 2: Dùng range
func printChars(s string) {
	for _, c := range s {
		fmt.Printf("%c ", c)
	}
	fmt.Println()
}

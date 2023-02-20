package main

import (
	"fmt"
	"strings"
)

// Ràng buộc (constraint) kiểu dữ liệu khi sử dụng generic
type Number interface {
	int | int8 | int16 | int32 | int64 | uint | uint8 | uint16 | uint32 | uint64 | float32 | float64
}

// Generic function
func sum[T Number](a, b T) T {
	return a + b
}

// func map qua từng phần tử trong slice và transform
func Map[T, V any](s []T, transform func(T) V) []V {
	output := make([]V, len(s))

	for i, v := range s {
		output[i] = transform(v)
	}

	return output
}

// func tìm index của phần tử trong slice
func indexOf[T comparable](s []T, x T) int {
	for i, v := range s {
		if v == x {
			return i
		}
	}

	return -1
}

func main() {
	var a, b int8 = 5, 6
	fmt.Println("sum(5, 6)", sum(a, b))

	var x, y float64 = 8.5, 7.3
	fmt.Println("sum(8.5, 7.3)", sum(x, y))

	// var c, d string = "Hello ", "Gopher"
	// sum(c, d) // báo lỗi vì dữ liệu nằm ngoài ràng buộc của hàm sum

	s1 := []int{1, 2, 3, 4}
	transformedS1 := Map(s1, func(v int) int { return v * 3 })
	fmt.Println(transformedS1)

	s2 := []string{"alice", "bob", "john"}
	transformedS2 := Map(s2, func(v string) string { return strings.ToUpper(v) })
	fmt.Println(transformedS2)

	fmt.Println("Find 3 in s1:", indexOf(s1, 3))
	fmt.Println("Find bob in s2:", indexOf(s2, "Bob"))
}

package main

import "fmt"

func main() {
	m1 := multiples(2, 1, 2, 3, 4, 5, 6)
	m2 := multiples(5, 3, 5, 7)
	fmt.Println("m1", m1)
	fmt.Println("m2", m2)

	// truyền slice vào variadic function
	nums1 := []int{10, 20, 30}
	nums2 := []int{40, 50, 60}
	nums3 := []int{70, 80, 90}
	nums1 = append(nums1, nums2...)
	nums1 = append(nums1, nums3...)

	m3 := multiples(3, nums1...)
	fmt.Println("m3", m3)
}

func multiples(x int, nums ...int) []int {
	s := make([]int, len(nums))

	for i, v := range nums {
		s[i] = v * x
	}

	return s
}

package main

import "fmt"

func main() {
	b := 156

	var a *int = &b
	fmt.Printf("type of a %T\n", a)
	fmt.Printf("address of a %v\n", a)
	fmt.Println("a:", a)
	// Thay đổi giá trị của con trỏ a
	*a++
	fmt.Printf("value of b: %d\n", b)

	var c *int
	fmt.Println("c:", c) // zero value của con trỏ là <nil>
	c = &b
	fmt.Println("c:", c)

	d := new(string) // zero value của string: ""
	fmt.Printf("value: %s, type: %T, address: %v\n", *d, d, d)
	*d = "Hello"

	x := 10
	increaseNumber(&x)
	fmt.Printf("x after call func %d\n", x)

	arr := [3]int{1, 50, 100}
	// modify(&arr, 0)
	modify(arr[:], 0)
	fmt.Println("array:", arr)

	z := 25
	var pZ = &z
	var ppZ = &pZ
	fmt.Println("z:", z, "address:", &z)
	fmt.Println("pZ:", pZ, "address:", &pZ, "value:", *pZ)
	fmt.Println("ppZ:", ppZ, "address:", &ppZ, "value:", **ppZ)

	y := 100
	var pY1 = &y
	var pY2 = &y
	if pY1 == pY2 {
		fmt.Println("*pY1", *pY1)
		fmt.Println("*pY2", *pY2)
	}

}

func increaseNumber(val *int) {
	*val += 1
}

// Cách 1: Truyền con trỏ array
// func modify(a *[3]int, index int) {
// 	// (*a)[index] = 100
// 	a[index] = 100
// }

// Cách 2: Truyền slice
func modify(s []int, index int) {
	s[index] = 100
}

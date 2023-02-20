package main

import (
	"fmt"
)

func main() {
	var a int
	var b *int

	a = 9
	b = &a
	fmt.Println(a, b)

}

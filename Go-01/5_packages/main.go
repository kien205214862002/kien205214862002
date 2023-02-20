package main

// Tạo module: go mod init <module-name>
// go install .
// tạo folder tương ứng với <package-name>

import (
	"fmt"
	_ "learnpackage/driver"
	math "learnpackage/mymath"
)

func main() {
	fmt.Println(math.PI)

	sum := math.Sum(5, 8)
	fmt.Println("Sum:", sum)

	area, perimeter := math.CircleProps(6.5)
	fmt.Printf("Area %.2f - Perimeter %.2f\n", area, perimeter)

}

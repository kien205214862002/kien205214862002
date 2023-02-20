package main

import (
	"fmt"
	"math"
)

type Rectangle struct {
	length, width float64
}

type Circle struct {
	radius float64
}

type Point struct {
	x, y float64
}

// Định nghĩa phương thức tính diện tích cho struct Rectangle
// func (receiver) name() type {}
func (p Rectangle) area() float64 {
	return p.length * p.width
}

func (c Circle) area() float64 {
	return math.Pi * c.radius * c.radius
}

// Định nghĩa phương thức translate cho struct Point
func (p *Point) translate(dx, dy float64) {
	// Thay đổi giá trị các fields của struct p
	// Bởi vì ta truyền receiver là con trỏ, nên sự thay đổi bên trong phương thức sẽ thể hiện ra bên ngoài phương thức
	p.x += dx
	p.y += dy
}

type myInt int // định nghĩa alias
// Định nghĩa phương thức add cho kiểu dữ liệu int
func (n myInt) add(x myInt) myInt {
	return n + x
}

func main() {
	r := Rectangle{5, 8.5}
	fmt.Printf("Rectangle area: %.2f\n", r.area())

	c := Circle{5.5}
	fmt.Printf("Circle area: %.2f\n", c.area())

	p := Point{1.5, 3.5}
	fmt.Println("p trước khi gọi phương thức transalte:", p)
	p.translate(3, 1.5)
	fmt.Println("p sau khi gọi phương thức transalte:", p)

	n := myInt(100)
	sum := n.add(30)
	fmt.Println("Sum:", sum)

}

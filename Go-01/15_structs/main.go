package main

import "fmt"

type Employee struct {
	firstName, lastName string
	age                 int
	salary              float64
}

type Pet struct {
	string // trường ẩn danh
	int
}

// Nested struct
type Address struct {
	country, city string
}

type Person struct {
	firstName string
	lastName  string
	Address   // anonymous filed
}

func main() {
	emp1 := Employee{"Dan", "Nguyen", 1000, 27}

	fmt.Println("emp1:", emp1)

	emp2 := Employee{"Khai", "Truong", 30, 1500}
	fmt.Println("emp2:", emp2)

	emp3 := Employee{}
	fmt.Println("emp3:", emp3)

	// Truy cập vào các trường của struct
	fmt.Printf("Emp1: First Name: %s, Last Name: %s, age: %d, salary: %.2f\n", emp1.firstName, emp1.lastName, emp1.age, emp1.salary)
	emp1.salary = 2000
	fmt.Printf("Emp1: new salary %.2f\n", emp1.salary)

	// anonymous struct (cấu trúc ẩn danh)
	account := struct {
		email, password string
	}{"dan@gmail.com", "123123"}
	fmt.Printf("Account: %s - %s\n", account.email, account.password)

	// con trỏ struct
	emp4 := &Employee{
		firstName: "Hieu",
		lastName:  "Dang",
		salary:    1500,
		age:       28,
	}
	// Truy cập giá trị các trường: thay vì (*emp4).firstName ta có thể viết emp4.firstName
	reviewSalary(emp4)
	fmt.Printf("Emp4: new salary %.2f\n", emp4.salary)

	// anonymous fields (trường ẩn danh)
	pet1 := Pet{"Kiki", 2}
	fmt.Printf("Pet1: Name %s - Age %d\n", pet1.string, pet1.int)

	ps1 := Person{
		firstName: "Tí",
		lastName:  "Nguyễn",
		Address: Address{
			country: "Vietnam",
			city:    "Ho Chi Minh",
		},
	}

	fmt.Printf("Person1: FullName: %s %s, Address: %s %s\n", ps1.firstName, ps1.lastName, ps1.city, ps1.country)
}

func reviewSalary(e *Employee) {
	e.salary += e.salary * 10 / 100
}

package main

import (
	"fmt"
	"time"
)

// built-in interface
//	type error interface {
//		Error() string
//	}

type AppError struct {
	Message string
	Code    int
	Time    time.Time
}

func (e *AppError) Error() string {
	return fmt.Sprintf("Code: %d - Message: %s - Time: %v", e.Code, e.Message, e.Time)
}

func run() (int, error) {
	// do something

	// return 1, nil

	return 0, &AppError{"Database error", 500, time.Now()}
}

type Student struct {
	Name      string
	ClassName string
}

func (s *Student) NewStudent(name, className string) (*Student, error) {
	s.Name = name
	s.ClassName = className

	return s, nil
	// return nil, &AppError{"Student not found", 500, time.Now()}
}

func main() {
	if i, err := run(); err != nil {
		fmt.Println(err)
	} else {
		// Không có lỗi xảy ra
		fmt.Println(i)
	}

	student := Student{}
	if _, err := student.NewStudent("Alice", "Golang"); err != nil {
		fmt.Println(err)
	} else {
		fmt.Println("Student", student)
	}
}

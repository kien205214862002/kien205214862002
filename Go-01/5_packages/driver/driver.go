package driver

import "fmt"

// Mỗi package go có thể chứa 1 func init, func này không nhận vào bất kì tham số nào và cũng không return về bất kì giá trị nào
// Hàm init không thể được gọi ở bất cứ đâu trong source code mà nó sẽ tự động được thực thi khi package được khởi tạo (khi nó được import)
func init() {
	fmt.Println("Driver installing...")
}

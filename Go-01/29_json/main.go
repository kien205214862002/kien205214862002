package main

import (
	"encoding/json"
	"fmt"
)

type Permission map[string]bool

type User struct {
	// export fields
	Username   string     `json:"username"`
	Email      string     `json:"email"`
	Permission Permission `json:"permission"`
}

func main() {
	user := User{
		Username:   "dannd",
		Email:      "dannd@gmail.com",
		Permission: Permission{"admin": true},
	}

	// Chuyển struct thành json
	// Chỉ có export fields mới được chuyển thành json
	j, err := json.Marshal(user)
	// j, err := json.MarshalIndent(user, "", "\t")
	if err != nil {
		fmt.Println(err)
		return
	}

	fmt.Println(string(j))

	// Chuyển json thành struct
	jsonStr := `{
		"username": "alice",
		"email": "alice@gmail.com",
		"permission": {
			"read": true,
			"write": true
		}
	}`

	var user1 User
	err = json.Unmarshal([]byte(jsonStr), &user1)
	if err != nil {
		panic(err)
	}
	fmt.Println(user1)
}

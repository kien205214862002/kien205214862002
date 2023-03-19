package middleware

import (
	"context"
	"go01-airbnb/config"
	usermodel "go01-airbnb/internal/user/model"
)

type UserRepository interface {
	FindDataWithCondition(context.Context, map[string]any) (*usermodel.User, error)
}

type middlewareManager struct {
	cfg      *config.Config
	userRepo UserRepository
}

// cfg *config.Config: một con trỏ đến một đối tượng cấu hình config.Config.
// userRepo UserRepository: một đối tượng UserRepository.
// Hàm trả về một con trỏ đến một đối tượng middleareManager mới được tạo với các tham số đầu vào được chuyển vào.
func NewMiddlewareManager(cfg *config.Config, userRepo UserRepository) *middlewareManager {
	return &middlewareManager{cfg, userRepo}
}

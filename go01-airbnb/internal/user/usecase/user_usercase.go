package userusecase

import (
	"context"
	"errors"
	"fmt"
	"go01-airbnb/config"
	usermodel "go01-airbnb/internal/user/model"
	"go01-airbnb/pkg/utils"
)

type UserRepository interface {
	Create(context.Context, *usermodel.UserCreate) error
	FindDataWithCondition(context.Context, map[string]any) (*usermodel.User, error)
}

type userUseCase struct {
	cfg            *config.Config
	userRepository UserRepository
}

func NewUserUseCase(cfg *config.Config, userRepository UserRepository) *userUseCase {
	return &userUseCase{cfg, userRepository}
}

func (u *userUseCase) Register(ctx context.Context, data *usermodel.UserCreate) error {
	// Kiếm tra xem email đã tồn tại trong hệ thống hay chưa
	user, _ := u.userRepository.FindDataWithCondition(ctx, map[string]any{"email": data.Email})
	if user != nil {
		return errors.New("email is existed")
	}

	// Validate data
	if err := data.Validate(); err != nil {
		return err
	}

	// Chuẩn bị data trước khi tạo user
	if err := data.PrepareCreate(); err != nil {
		return err
	}
	fmt.Println("Password:", data.Password)

	if err := u.userRepository.Create(ctx, data); err != nil {
		return errors.New("cannot create your account")
	}

	return nil
}

// B1: Tìm user thông qua email
// B2: Compare password của user với hased password trong db
// B3: Generate token
func (u *userUseCase) Login(ctx context.Context, data *usermodel.UserLogin) (*utils.Token, error) {
	user, err := u.userRepository.FindDataWithCondition(ctx, map[string]any{"email": data.Email})
	if err != nil {
		return nil, errors.New("email or password invalid")
	}

	fmt.Println(user.Password, data.Password)
	if err := utils.ComparePassword(user.Password, data.Password); err != nil {
		return nil, errors.New("email or password invalid")
	}

	token, err := utils.GenerateJWT(utils.TokenPayload{Email: user.Email, Role: user.Role}, u.cfg)
	if err != nil {
		return nil, errors.New("internal server error")
	}

	return token, nil
}

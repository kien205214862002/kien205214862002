package userhttp

import (
	"context"
	usermodel "go01-airbnb/internal/user/model"
	"go01-airbnb/pkg/utils"

	"github.com/gin-gonic/gin"
)

type UserUsecase interface {
	Register(context.Context, *usermodel.UserCreate) error
	Login(context.Context, *usermodel.UserLogin) (*utils.Token, error)
}

type userHandler struct {
	userUC UserUsecase
}

func NewUserHandler(userUC UserUsecase) *userHandler {
	return &userHandler{userUC}
}

func (hdl *userHandler) Register() gin.HandlerFunc {
	return func(c *gin.Context) {
		var data usermodel.UserCreate

		if err := c.ShouldBind(&data); err != nil {
			c.JSON(400, gin.H{"error": err.Error()})
			return
		}

		if err := hdl.userUC.Register(c.Request.Context(), &data); err != nil {
			c.JSON(400, gin.H{"error": err.Error()})
			return
		}

		c.JSON(200, gin.H{"data": data.Id})
	}
}

func (hdl *userHandler) Login() gin.HandlerFunc {
	return func(c *gin.Context) {
		var credentials usermodel.UserLogin

		if err := c.ShouldBind(&credentials); err != nil {
			c.JSON(400, gin.H{"error": err.Error()})
			return
		}

		token, err := hdl.userUC.Login(c.Request.Context(), &credentials)
		if err != nil {
			c.JSON(400, gin.H{"error": err.Error()})
			return
		}

		c.JSON(200, gin.H{"data": token})
	}
}

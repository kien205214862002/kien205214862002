package middleware

import (
	usermodel "go01-airbnb/internal/user/model"
	"net/http"

	"github.com/gin-gonic/gin"
)

// Cần phải gọi middlware RequiredAuth trước
func (m *middleareManager) RequiredRoles(roles ...string) gin.HandlerFunc {
	return func(c *gin.Context) {
		user := c.MustGet("user").(*usermodel.User)

		for i := range roles {
			if user.GetUserRole() == roles[i] {
				c.Next()
				return
			}
		}

		c.JSON(http.StatusForbidden, gin.H{"error": "you have no permission"})
	}
}

package middleware

import (
	"errors"
	"go01-airbnb/pkg/utils"
	"net/http"
	"strings"

	"github.com/gin-gonic/gin"
)

// Extract bearer token from request Authorization header
func extractTokenFromHeader(r *http.Request) (string, error) {
	// bearerToken: Bearer abcxyz...
	bearerToken := r.Header.Get("Authorization")
	parts := strings.Split(bearerToken, " ")

	if parts[0] != "Bearer" || len(parts) < 2 || strings.TrimSpace(parts[1]) == "" {
		return "", errors.New("invalid token")
	}

	return parts[1], nil
}

// middleware kiểm tra xem token có hợp lệ hay không
// B1: Get token from header
// B2: Validate token và get payload
// B3: Từ payload, dùng email để tìm user trong DB
func (m *middleareManager) RequiredAuth() gin.HandlerFunc {
	return func(c *gin.Context) {
		token, err := extractTokenFromHeader(c.Request)
		if err != nil {
			c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
			return
		}

		payload, err := utils.ValidateJWT(token, m.cfg)
		if err != nil {
			c.JSON(http.StatusUnauthorized, gin.H{"error": "Unauthorized"})
			return
		}

		user, err := m.userRepo.FindDataWithCondition(c.Request.Context(), map[string]any{"email": payload.Email})
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": "Internal Server Error"})
			return
		}

		// Lưu trữ data để có thể được truy cập ở các middleware hoặc handler tiếp theo
		c.Set("user", user)

		// Chuyển qua middleare hoặc handler tiếp theo
		c.Next()
	}
}

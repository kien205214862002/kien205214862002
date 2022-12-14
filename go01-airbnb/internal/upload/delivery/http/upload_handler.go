package uploadhttp

import (
	"fmt"
	"go01-airbnb/pkg/common"
	"net/http"

	"github.com/gin-gonic/gin"
)

type uploadHandler struct{}

func NewUploadHandler() *uploadHandler {
	return &uploadHandler{}
}

func (hdl *uploadHandler) Upload() gin.HandlerFunc {
	return func(c *gin.Context) {
		fileHeader, err := c.FormFile("file")
		if err != nil {
			panic(common.ErrBadRequest(err))
		}

		if err := c.SaveUploadedFile(fileHeader, fmt.Sprintf("static/%s", fileHeader.Filename)); err != nil {
			panic(err)
		}

		c.JSON(http.StatusOK, common.Response(common.Image{
			Url: "http://localhost:4000/static/" + fileHeader.Filename,
		}))
	}
}

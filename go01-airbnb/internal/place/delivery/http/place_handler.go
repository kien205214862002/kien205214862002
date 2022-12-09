package placehttp

import (
	"context"
	placemodel "go01-airbnb/internal/place/model"
	"go01-airbnb/pkg/common"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
)

type PlaceUseCase interface {
	CreatePlace(context.Context, *placemodel.Place) error
	GetPlaces(context.Context, *common.Paging, *placemodel.Filter) ([]placemodel.Place, error)
	GetPlaceByID(context.Context, int) (*placemodel.Place, error)
	UpdatePlace(context.Context, int, *placemodel.Place) error
	DeletePlace(context.Context, int) error
}

type placeHandler struct {
	placeUC PlaceUseCase
}

func NewPlaceHandler(placeUC PlaceUseCase) *placeHandler {
	return &placeHandler{placeUC}
}

func (hdl *placeHandler) CreatePlace() gin.HandlerFunc {
	return func(c *gin.Context) {
		var place placemodel.Place

		if err := c.ShouldBind(&place); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		if err := hdl.placeUC.CreatePlace(c.Request.Context(), &place); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"data": place})
	}
}

func (hdl *placeHandler) GetPlaces() gin.HandlerFunc {
	return func(c *gin.Context) {
		// paging
		var paging common.Paging
		if err := c.ShouldBind(&paging); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}
		paging.Fulfill()

		// filter
		var filter placemodel.Filter
		if err := c.ShouldBind(&filter); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		data, err := hdl.placeUC.GetPlaces(c.Request.Context(), &paging, &filter)
		if err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"data": data, "paging": paging})
	}
}

func (hdl *placeHandler) GetPlaceByID() gin.HandlerFunc {
	return func(c *gin.Context) {
		id, err := strconv.Atoi(c.Param("id"))
		if err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		data, err := hdl.placeUC.GetPlaceByID(c.Request.Context(), id)
		if err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"data": data})
	}
}

func (hdl *placeHandler) UpdatePlace() gin.HandlerFunc {
	return func(c *gin.Context) {
		id, err := strconv.Atoi(c.Param("id"))
		if err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		var place placemodel.Place

		if err := c.ShouldBind(&place); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		if err := hdl.placeUC.UpdatePlace(c.Request.Context(), id, &place); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"data": place})
	}
}

func (hdl *placeHandler) DeletePlace() gin.HandlerFunc {
	return func(c *gin.Context) {
		id, err := strconv.Atoi(c.Param("id"))
		if err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		if err := hdl.placeUC.DeletePlace(c.Request.Context(), id); err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
			return
		}

		c.JSON(http.StatusOK, gin.H{"data": true})
	}
}

package placemodel

import (
	"errors"
	"go01-airbnb/pkg/common"
	"strings"
)

type Place struct {
	common.SQLModel
	Owner_Id      int     `json:"ownerId" gorm:"column:owner_id"`
	City_Id       int     `json:"cityId" gorm:"column:city_id"`
	Name          string  `json:"name" gorm:"column:name"`
	Address       string  `json:"address" gorm:"column:address"`
	Lat           float64 `json:"lat" gorm:"column:lat"`
	Lng           float64 `json:"lng" gorm:"column:lng"`
	PricePerNight float64 `json:"pricePerNight" gorm:"column:price_per_night"`
}

func (Place) TableName() string {
	return "places"
}

func (p *Place) Validate() error {
	p.Name = strings.TrimSpace(p.Name)
	if p.Name == "" {
		return errors.New("place name can't be blank")
	}

	p.Address = strings.TrimSpace(p.Address)
	if p.Address == "" {
		return errors.New("place address can't be blank")
	}

	return nil
}

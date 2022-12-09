package placerepository

import (
	"golang.org/x/net/context"
	"gorm.io/gorm"
	"gorm.io/gorm/clause"

	placemodel "go01-airbnb/internal/place/model"
	"go01-airbnb/pkg/common"
)

type placeRepository struct {
	db *gorm.DB
}

// Constructor
func NewPlaceRepository(db *gorm.DB) *placeRepository {
	return &placeRepository{db}
}

// Create place
func (r *placeRepository) Create(ctx context.Context, place *placemodel.Place) error {
	db := r.db.Begin()

	if err := db.Table(place.TableName()).Create(place).Error; err != nil {
		db.Rollback()
		return common.NewErrorDB(err)
	}

	if err := db.Commit().Error; err != nil {
		db.Rollback()
		return common.NewErrorDB(err)
	}

	return nil
}

// Get places
func (r *placeRepository) ListDataWithCondition(ctx context.Context, paging *common.Paging, filter *placemodel.Filter) ([]placemodel.Place, error) {
	var data []placemodel.Place

	db := r.db.Table(placemodel.Place{}.TableName())

	if v := filter.OwnerId; v > 0 {
		db = db.Where("owner_id = ?", v)
	}

	if v := filter.CityId; v > 0 {
		db = db.Where("city_id = ?", v)
	}

	if v := paging.Cursor; v != 0 {
		db = db.Where("id > ?", v)
	} else {
		db = db.Offset((paging.Page - 1) * paging.Limit)
	}

	if err := db.Count(&paging.Total).Limit(paging.Limit).Find(&data).Error; err != nil {
		return nil, err
	}

	if len(data) > 0 {
		paging.NextCursor = data[len(data)-1].Id
	}

	return data, nil
}

// Get place by condition
func (r *placeRepository) FindDataWithCondition(ctx context.Context, condition map[string]any) (*placemodel.Place, error) {
	var data placemodel.Place

	if err := r.db.Where(condition).First(&data).Error; err != nil {
		return nil, common.ErrEntityNotFound("place", err)
	}

	return &data, nil
}

// Update palce by condition
func (r *placeRepository) Update(ctx context.Context, condition map[string]any, place *placemodel.Place) error {
	db := r.db.Table(placemodel.Place{}.TableName())

	if err := db.Model(&placemodel.Place{}).Clauses(clause.Returning{}).Where(condition).Updates(place).Error; err != nil {
		return err
	}

	return nil
}

// Delete place by condition
func (r *placeRepository) Delete(ctx context.Context, condition map[string]any) error {
	if err := r.db.Table(placemodel.Place{}.TableName()).Where(condition).Delete(&placemodel.Place{}).Error; err != nil {
		return err
	}

	return nil
}

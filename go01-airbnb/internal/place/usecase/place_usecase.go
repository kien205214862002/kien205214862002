package placeusecase

import (
	"context"
	placemodel "go01-airbnb/internal/place/model"
	"go01-airbnb/pkg/common"
)

type PlaceRepository interface {
	Create(context.Context, *placemodel.Place) error
	ListDataWithCondition(context.Context, *common.Paging, *placemodel.Filter) ([]placemodel.Place, error)
	FindDataWithCondition(context.Context, map[string]any) (*placemodel.Place, error)
	Update(context.Context, map[string]any, *placemodel.Place) error
	Delete(context.Context, map[string]any) error
}

type placeUseCase struct {
	placeRepo PlaceRepository
}

func NewPlaceUseCase(placeRepo PlaceRepository) *placeUseCase {
	return &placeUseCase{placeRepo}
}

func (uc *placeUseCase) CreatePlace(ctx context.Context, place *placemodel.Place) error {
	if err := place.Validate(); err != nil {
		return err
	}

	if err := uc.placeRepo.Create(ctx, place); err != nil {
		return err
		// return common.ErrCannotCreateEntity("place", err)
	}

	return nil
}

func (uc *placeUseCase) GetPlaces(ctx context.Context, paging *common.Paging, filter *placemodel.Filter) ([]placemodel.Place, error) {
	// business logic

	data, err := uc.placeRepo.ListDataWithCondition(ctx, paging, filter)

	if err != nil {
		return nil, err
	}

	return data, nil
}

func (uc *placeUseCase) GetPlaceByID(ctx context.Context, id int) (*placemodel.Place, error) {
	data, err := uc.placeRepo.FindDataWithCondition(ctx, map[string]any{"id": id})
	if err != nil {
		return nil, err
	}

	return data, nil
}

func (uc *placeUseCase) UpdatePlace(ctx context.Context, id int, place *placemodel.Place) error {
	if err := place.Validate(); err != nil {
		return err
	}

	_, err := uc.placeRepo.FindDataWithCondition(ctx, map[string]any{"id": id})
	if err != nil {
		return err
	}

	if err := uc.placeRepo.Update(ctx, map[string]any{"id": id}, place); err != nil {
		return err
	}

	return nil
}

func (uc *placeUseCase) DeletePlace(ctx context.Context, id int) error {
	_, err := uc.placeRepo.FindDataWithCondition(ctx, map[string]any{"id": id})
	if err != nil {
		return err
	}

	if err := uc.placeRepo.Delete(ctx, map[string]any{"id": id}); err != nil {
		return err
	}

	return nil
}

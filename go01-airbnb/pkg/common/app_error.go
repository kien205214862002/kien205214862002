package common

import (
	"fmt"
	"net/http"
)

type AppError struct {
	Causes     error  `json:"-"` // Lỗi gốc của chương trình
	StatusCode int    `json:"status"`
	Message    string `json:"message"`
	// Dùng làm đa ngôn ngữ
	// Key    string `json:"key"`
}

func NewErrorResponse(causes error, status int, msg string) *AppError {
	return &AppError{causes, status, msg}
}

func (e *AppError) RootCauses() error {
	if err, ok := e.Causes.(*AppError); ok {
		return err.RootCauses()
	}

	return e.Causes
}

func (e *AppError) Error() string {
	return e.RootCauses().Error()
}

func NewUnauthorizedError(causes error, msg string) *AppError {
	return NewErrorResponse(causes, http.StatusUnauthorized, msg)
}

func NewForbiddenError(causes error, msg string) *AppError {
	return NewErrorResponse(causes, http.StatusForbidden, msg)
}

func NewBadRequestError(causes error, msg string) *AppError {
	return NewErrorResponse(causes, http.StatusForbidden, msg)
}

func NewErrorDB(causes error) *AppError {
	return &AppError{causes, http.StatusInternalServerError, "something went wrong with DB"}
}

func NewCustomError(causes error, msg string) *AppError {
	return &AppError{causes, http.StatusBadRequest, msg}
}

func ErrCannotListEntity(entity string, causes error) *AppError {
	return NewCustomError(causes, fmt.Sprintf("Cannot list %s", entity))
}

func ErrEntityNotFound(entity string, causes error) *AppError {
	return NewCustomError(causes, fmt.Sprintf("%s not found", entity))
}

//func ErrEntityNotFound(entity string, causes error) *AppError {
//	return NewCustomError(causes, fmt.Sprintf("%s not found", entity))
//}

// ErrCannotGetEntity, ErrCannotCreateEntity, ErrCannotUpdateEntity, ErrCannotDeleteEntity

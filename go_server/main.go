package main

import (
	"net/http"
	"os"
	"path/filepath"

	"github.com/gin-gonic/gin"
)

const (
	// Path to images
	catImagesPath = "./../server/static/cats/"
)

func GetInfoJson(c *gin.Context) {
	id := c.Param("id")
	filePath := filepath.Join(catImagesPath, id, "info.json")

	data, err := os.ReadFile(filePath)
	if err != nil {
		c.JSON(http.StatusNotFound, gin.H{
			"error": "File not found",
		})
		return
	}

	c.Data((http.StatusOK), "application/json", data)
}

func main() {
	router := gin.Default()
	router.GET("/ping", func(c *gin.Context) {
		c.AsciiJSON(200, gin.H{
			"message": "pong",
		})
	})

	router.GET("/:id/info.json", GetInfoJson)
	router.Run(":8080")
}

package main

import (
	"io/fs"
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

	c.Data(http.StatusOK, "application/json", data)
}

func GetCatalogue(c *gin.Context) {
	imageDirFS := os.DirFS(catImagesPath)

	images, err := fs.ReadDir(imageDirFS, ".")
	if err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{
			"error": "Unable to read directory",
		})
		return
	}

	catalogue := make([]string, 0, len(images))
	for _, image := range images {
		catalogue = append(catalogue, image.Name())
	}

	c.JSON(http.StatusOK, gin.H{
		"ImageIds": catalogue,
	})
}

func main() {
	router := gin.Default()
	router.GET("/ping", func(c *gin.Context) {
		c.AsciiJSON(200, gin.H{
			"message": "pong",
		})
	})

	router.GET("/:id/info.json", GetInfoJson)
	router.GET("/catalogue", GetCatalogue)
	router.Run(":8080")
}

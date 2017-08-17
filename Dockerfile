FROM microsoft/dotnet:1.1.0-runtime-deps

COPY ProcessoEletronicoWebAPI/src/WebAPI/publish /home/bin
WORKDIR /home/bin

EXPOSE 3308/tcp

CMD ["./WebAPI"]

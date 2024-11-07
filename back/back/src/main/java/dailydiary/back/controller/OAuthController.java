package dailydiary.back.controller;

import dailydiary.back.diaryservice.service.user.UserService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.util.UriComponentsBuilder;

import java.util.Map;

@Slf4j
@RestController
@RequestMapping("/api")
public class OAuthController {

    private final UserService userService;

    @Autowired
    public OAuthController(UserService userService) {  // UserService 주입 설정
        this.userService = userService;
    }

    @Value("${oauth.client-id}")
    private String clientId;

    @Value("${oauth.auth-uri}")
    private String authUri;

    @GetMapping("/google-login")
    public ResponseEntity<String> getGoogleLoginUrl() {
        log.info("Received GET request on /google-login");

        String redirectUri = "http://localhost:5000/api/google-login/callback";

        String googleLoginUrl = UriComponentsBuilder.fromHttpUrl(authUri)
                .queryParam("client_id", clientId)
                .queryParam("redirect_uri", redirectUri)
                .queryParam("response_type", "code")
                .queryParam("scope", "openid profile email")
                .build().toUriString();

        log.info("Generated Google login URL: {}", googleLoginUrl);

        return ResponseEntity.ok(googleLoginUrl);
    }

    @PostMapping("/google-login")
    public ResponseEntity<String> saveUserSession(@RequestBody Map<String, String> requestBody) {
        String accessToken = requestBody.get("accessToken");

        if (accessToken == null) {
            log.error("No access token provided");
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body("Access token is required.");
        }

        log.info("Received POST request on /google-login with access token");

        // UserService를 사용하여 MongoDB에 저장
        userService.saveUserSession(accessToken);
        log.info("User session information stored in MongoDB");

        return ResponseEntity.ok("User session information stored successfully");
    }
}
